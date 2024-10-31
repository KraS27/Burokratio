using System.Linq.Expressions;
using Application.Interfaces;
using Core.Entities;
using Core.Primitives;
using Core.ValueObjects;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NotarRepository : INotarRepository
    {
        private readonly AppDbContext _dbContext;

        public NotarRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Notar notar, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<Notar>().
                AddAsync(notar, cancellationToken);           
        }

        public async Task DeleteAsync(Notar notar, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<Notar>().
               Remove(notar!);            
        }

        public async Task<Result<PagedResponse<Notar>>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Notar>()
                .AsNoTracking()
                .ToPagedResponseAsync(pagination);
        }

        public async Task<Notar?> GetByEmailOrPhoneAsync(Email email, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Notar>()
                .FirstOrDefaultAsync(n => n.Email.Equals(email) || n.PhoneNumber.Equals(phoneNumber), cancellationToken);
        }

        public async Task<Notar?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Notar>()
                .FirstOrDefaultAsync(n => n.Email.Equals(email), cancellationToken);
        }

        public async Task<Notar?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
                return await _dbContext.Set<Notar>()
                        .FindAsync(id, cancellationToken);
        }

        public async Task<Notar?> GetByPhoneAsync(PhoneNumber number, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Notar>()
               .FirstOrDefaultAsync(n => n.PhoneNumber!.Equals(number), cancellationToken);
        }        
    }       
}
