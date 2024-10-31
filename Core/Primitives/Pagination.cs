namespace Core.Primitives;

public class Pagination
{
    public const int MAX_PAGE_SIZE = 30;
    
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 15;
}