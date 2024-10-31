using Core.Errors;
using Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class PaginationExtensions
{
    public static async Task<Result<PagedResponse<T>>> ToPagedResponseAsync<T>(this IQueryable<T> query, 
        Pagination pagination,
        CancellationToken cancellationToken = default)
    {
        if (pagination.PageSize < 0 || pagination.PageSize > Pagination.MAX_PAGE_SIZE)
            return PaginationErrors.InvalidPageSize();
        
        if(pagination.PageNumber < 1)
            return PaginationErrors.InvalidPageNumber();
        
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