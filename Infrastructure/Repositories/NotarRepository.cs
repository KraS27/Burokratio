﻿using Application.Interfaces;
using Core.Entities;
using Core.ValueObjects;
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

        public async Task<ICollection<Notar>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Notar>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
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
