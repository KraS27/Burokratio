using Core.Entities;

namespace Application.Interfaces
{
    public interface INotarRepository
    {
        Task<Notar?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<ICollection<Notar>> GetAllAsync(CancellationToken cancellationToken);

        Task AddAsync(Notar notar, CancellationToken cancellationToken);

        Task UpdateAsync(Notar notar, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
