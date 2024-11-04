using Application.DTO.Notar;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Errors;
using Core.Primitives;
using Core.ValueObjects;

namespace Application.Services
{
    public class NotarService
    {
        private readonly INotarRepository _notarRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotarService(INotarRepository notarRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _notarRepository = notarRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<NotarResponse?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var notar = await _notarRepository
                .GetByIdAsync(id, cancellationToken);
            
            if (notar == null)
                NotarErrors.NotFound(id);
            
            var notarResponse = _mapper.Map<NotarResponse?>(notar);

            return notarResponse;
        }
        public async Task<Result<PagedResponse<NotarResponse>>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken)
        {
            if (pagination.PageSize is < 0 or > Pagination.MAX_PAGE_SIZE)
                return PaginationErrors.InvalidPageSize();
        
            if(pagination.PageNumber < 1)
                return PaginationErrors.InvalidPageNumber();
            
            var notars = await _notarRepository.GetAllAsync(pagination, cancellationToken);
            var notarsCount = await _notarRepository.GetCountAsync(cancellationToken);
            
            var response = new PagedResponse<NotarResponse>
            {
                TotalCount = notarsCount,
                PageSize = pagination.PageSize,
                CurrentPage = pagination.PageNumber,
                Items = _mapper.Map<List<NotarResponse>>(notars)
            };
            
            return Result<PagedResponse<NotarResponse>>.Success(response);
        }
        public async Task<Result<Guid>> AddAsync(CreateNotarRequest request, CancellationToken cancellationToken)
        {
           var emailAndPhoneResult =  await CheckEmailAndPhoneAsync(request.Email, request.PhoneNumber,cancellationToken);

           if (emailAndPhoneResult.IsFailure)
               return emailAndPhoneResult.Error!;
           
           (Email email, PhoneNumber phone) = emailAndPhoneResult.Value;
           
            var addressResult = Address.Create(request.Division, request.Country, request.City, request.Street, request.PostalCode);
            var coordinatesResult = Coordinates.Create(request.Latitude, request.Longitude);

            if(addressResult.IsFailure || coordinatesResult.IsFailure)
                return addressResult.Error ?? coordinatesResult.Error!;
            
            Result<Notar> notarResult = Notar.Create(
                request.Name, 
                addressResult.Value!, 
                coordinatesResult.Value!,
                email,
                phone
                ).Value!;

            if (notarResult.IsFailure)
                return notarResult.Error!;

            await _notarRepository.AddAsync(notarResult.Value!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return notarResult.Value!.Id;
        }     
        public async Task<Result> UpdateAsync(UpdateNotarRequest request, CancellationToken cancellationToken)
        {
            var notar = await _notarRepository.GetByIdAsync(request.Id, cancellationToken);

            if (notar == null)
                return NotarErrors.NotFound(request.Id);

            Email email;
            if (notar.Email.Value != request.Email)
            {
                var emailResult = await CheckEmailAsync(request.Email, cancellationToken);

                if (emailResult.IsFailure)
                    return emailResult.Error!;

                email = emailResult.Value!;
            }
            else
                email = notar.Email;
            
            PhoneNumber phoneNumber;
            if (notar.PhoneNumber.Value != request.PhoneNumber)
            {
                var phoneResult = await CheckPhoneAsync(request.PhoneNumber, cancellationToken);

                if (phoneResult.IsFailure)
                    return phoneResult.Error!;

                phoneNumber = phoneResult.Value!;
            }
            else
                phoneNumber = notar.PhoneNumber;
            
            var addressResult = Address.Create(request.Division, request.Country, request.City, request.Street, request.PostalCode);
            var coordinatesResult = Coordinates.Create(request.Latitude, request.Longitude);

            if (addressResult.IsFailure || coordinatesResult.IsFailure)
                return addressResult.Error ?? coordinatesResult.Error!;

            var updateResult = notar.Update(request.Name,
                addressResult.Value!,
                coordinatesResult.Value!,
                email,
                phoneNumber);

            if (updateResult.IsFailure)
                return updateResult.Error!;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var notar = await _notarRepository.GetByIdAsync(id, cancellationToken);

            if (notar == null)
                return NotarErrors.NotFound(id);

            await _notarRepository.DeleteAsync(notar, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }       
        private async Task<Result<(Email email, PhoneNumber phoneNumber)>> CheckEmailAndPhoneAsync(string email, string phone, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(email);
            var phoneResult = PhoneNumber.Create(phone);
            
            if (emailResult.IsFailure || phoneResult.IsFailure)
                return emailResult.Error ?? phoneResult.Error!;
            
            var notar = await _notarRepository.GetByEmailOrPhoneAsync(emailResult.Value!, phoneResult.Value!, cancellationToken);
            
            if (notar != null && notar.Email.Equals(emailResult.Value))
              return NotarErrors.EmailConflict(notar.Email.Value);
            
            if(notar != null && notar.PhoneNumber.Equals(phoneResult.Value))
                return NotarErrors.PhoneNumberConflict(notar.PhoneNumber.Value);
            
            return Result<(Email email, PhoneNumber phoneNumber)>.Success((emailResult.Value!, phoneResult.Value!));
        }
        private async Task<Result<Email>> CheckEmailAsync(string email, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(email);

            if (emailResult.IsFailure)
                return emailResult.Error!;

            var notar = await _notarRepository.GetByEmailAsync(emailResult.Value!, cancellationToken);

            if (notar != null)
                return NotarErrors.EmailConflict(notar.Email.Value);

            return emailResult;
        }
        private async Task<Result<PhoneNumber>> CheckPhoneAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            var phoneResult = PhoneNumber.Create(phoneNumber);

            if (phoneResult.IsFailure)
                return phoneResult.Error!;

            var notar = await _notarRepository.GetByPhoneAsync(phoneResult.Value!, cancellationToken);

            if (notar != null)
                return NotarErrors.PhoneNumberConflict(notar.PhoneNumber.Value);

            return phoneResult;
        }
    }
}
