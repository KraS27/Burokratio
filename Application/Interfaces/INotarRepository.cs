using Core.Entities;
using Core.ValueObjects;

namespace Application.Interfaces
{
    public interface INotarRepository
    {
        Task<Notar?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Notar?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

        Task<Notar?> GetByPhoneAsync(PhoneNumber number, CancellationToken cancellationToken);

        Task<ICollection<Notar>> GetAllAsync(CancellationToken cancellationToken);

        Task AddAsync(Notar notar, CancellationToken cancellationToken);

        Task UpdateAsync(Notar notar, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
