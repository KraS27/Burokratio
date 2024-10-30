using Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class PaginationExtensions
{
    public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(this IQueryable<T> query, 
        Pagination pagination,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync();
        
        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
        
        return new PagedResponse<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageSize = pagination.PageSize,
            CurrentPage = pagination.PageNumber
        };
    }
}