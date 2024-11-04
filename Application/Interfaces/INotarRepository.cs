using Core.Entities;
using Core.Primitives;
using Core.ValueObjects;

namespace Application.Interfaces
{
    public interface INotarRepository
    {
        Task<Notar?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        
        Task<Notar?> GetByEmailOrPhoneAsync(Email email, PhoneNumber phoneNumber, CancellationToken cancellationToken = default);

        Task<Notar?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

        Task<Notar?> GetByPhoneAsync(PhoneNumber number, CancellationToken cancellationToken = default);

        Task<ICollection<Notar>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken = default);

        Task AddAsync(Notar notar, CancellationToken cancellationToken = default);

        Task DeleteAsync(Notar notar, CancellationToken cancellationToken = default);
    }
}
