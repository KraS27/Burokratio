using Core.Primitives;

namespace Core.Errors;

public class PaginationErrors
{
    public static Error InvalidPageSize() => 
        Error.Validation($"Page size must be between 1 and {Pagination.MAX_PAGE_SIZE}.");
            
    public static Error InvalidPageNumber() => 
        Error.Validation($"Page number must be greater than 0.");
}