using Core.Entities;
using Core.ValueObjects;

namespace Application.Interfaces
{
    public interface INotarRepository
    {
        Task<Notar?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Notar?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

        Task<Notar?> GetByPhoneAsync(PhoneNumber number, CancellationToken cancellationToken = default);

        Task<ICollection<Notar>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Notar notar, CancellationToken cancellationToken = default);

        Task UpdateAsync(Notar notar, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
