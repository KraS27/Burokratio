using Application.DTO.Notar;
using Application.Interfaces;
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

        public NotarService(INotarRepository notarRepository, IUnitOfWork unitOfWork)
        {
            _notarRepository = notarRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Notar?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var notar = await _notarRepository.GetByIdAsync(id, cancellationToken);

            if (notar == null)
                NotarErrors.NotFound(id);

            return notar;
        }

        public async Task<Result<ICollection<Notar>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var notars = await _notarRepository.GetAllAsync(cancellationToken);
          
            return Result<ICollection<Notar>>.Success(notars);
        }

        public async Task<Result<Guid>> AddAsync(CreateNotarRequest request, CancellationToken cancellationToken)
        {
            var emailResult = await CheckEmailAsync(request.email, cancellationToken);
            var phoneResult = await CheckPhoneAsync(request.phoneNumber, cancellationToken);
            
            if (emailResult.IsFailure || phoneResult.IsFailure)
                return emailResult.Error ?? phoneResult.Error!;

            var addressResult = Address.Create(request.division, request.country, request.city, request.street, request.postalCode);
            var coordinatesResult = Coordinates.Create(request.latitude, request.longitude);

            if(addressResult.IsFailure || coordinatesResult.IsFailure)
                return addressResult.Error ?? coordinatesResult.Error!;
           
            Result<Notar> notarResult = Notar.Create(
                request.name, 
                addressResult.Value!, 
                coordinatesResult.Value!,
                emailResult.Value!,
                phoneResult.Value).Value!;

            if (notarResult.IsFailure)
                return notarResult.Error!;

            await _notarRepository.AddAsync(notarResult.Value!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return notarResult.Value!.Id;
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
                return NotarErrors.PhoneNumberConflict(notar.PhoneNumber!.Number);

            return phoneResult;
        }
        public async Task<Result> UdpateAsync(UpdateNotarRequest request, CancellationToken cancellationToken)
        {
            var notar = await _notarRepository.GetByIdAsync(request.id, cancellationToken);

            if (notar == null)
                return NotarErrors.NotFound(request.id);

            var emailTask = notar.Email.Value != request.email
                ? CheckEmailAsync(request.email, cancellationToken)
                : Task.FromResult(Result<Email>.Success(notar.Email));
          
            var phoneTask = notar.PhoneNumber.Number != request.phoneNumber
                ? CheckPhoneAsync(request.phoneNumber, cancellationToken)
                : Task.FromResult(Result<PhoneNumber>.Success(notar.PhoneNumber));

            await Task.WhenAll(emailTask, phoneTask);

            var emailResult = await emailTask;
            var phoneResult = await phoneTask;

            if (emailResult.IsFailure || phoneResult.IsFailure)
                return emailResult.Error ?? phoneResult.Error!;

            var addressResult = Address.Create(request.division, request.country, request.city, request.street, request.postalCode);
            var coordinatesResult = Coordinates.Create(request.latitude, request.longitude);

            if (addressResult.IsFailure || coordinatesResult.IsFailure)
                return addressResult.Error ?? coordinatesResult.Error!;

            var updateResult = notar.Update(request.name,
                addressResult.Value!,
                coordinatesResult.Value!,
                emailResult.Value!,
                phoneResult.Value);

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

            await _notarRepository.DeleteAsync(notar!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }       
    }
}
