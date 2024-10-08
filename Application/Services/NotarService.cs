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

        public NotarService(INotarRepository notarRepository)
        {
            _notarRepository = notarRepository;
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
            var emailTask = CheckEmailAsync(request.email, cancellationToken);
            var phoneTask = CheckPhoneAsync(request.phoneNumber, cancellationToken);
            
            await Task.WhenAll(emailTask, phoneTask);          

            var addressResult = Address.Create(request.division, request.country, request.city, request.street, request.postalCode);
            var coordinatesResult = Coordinates.Create(request.latitude, request.longitude);

            if(addressResult.IsFailure || coordinatesResult.IsFailure)
                return addressResult.Error ?? coordinatesResult.Error!;
           
            var notar = Notar.Create(
                request.name, 
                addressResult.Value!, 
                coordinatesResult.Value!,
                emailTask.Result.Value!,
                phoneTask.Result.Value).Value;

            await _notarRepository.AddAsync(notar!, cancellationToken);

            return notar!.Id;
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
    }
}
