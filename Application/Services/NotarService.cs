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
            //TODO: CHECK If email and phone number Unique

            var address = Address.Create(request.division, request.country, request.city, request.street, request.postalCode);

            if (address.IsFailure)
                return address.Error!;

            var coordinates = Coordinates.Create(request.latitude, request.longitude);

            if(coordinates.IsFailure)
                return coordinates.Error!;

            var email = Email.Create(request.email);

            if (email.IsFailure)
                return email.Error!;

            var phoneNumber = PhoneNumber.Create(request.phoneNumber);

            if (phoneNumber.IsFailure)
                return phoneNumber.Error!;

            var notar = Notar.Create(
                request.name, 
                address.Value!, 
                coordinates.Value!, 
                email.Value!, 
                phoneNumber.Value).Value;

            await _notarRepository.AddAsync(notar!, cancellationToken);

            return notar!.Id;
        }
    }
}
