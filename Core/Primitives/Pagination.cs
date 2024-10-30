namespace Core.Primitives;

public struct Pagination

{
    public Pagination()
    {
    }

    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 15;
}