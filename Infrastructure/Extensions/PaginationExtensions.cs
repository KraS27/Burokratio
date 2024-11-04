using Core.Errors;
using Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class PaginationExtensions
{
    public static async Task<ICollection<T>> ApplyPaginationAsync<T>(this IQueryable<T> query, 
        Pagination pagination,
        CancellationToken cancellationToken = default)
    {
        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return items;
    }
}