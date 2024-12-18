namespace Core.Primitives;

public class PagedResponse<T>
{
    public int TotalCount { get; set; } 
    public int PageSize { get; set; } 
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasPreviousPage => CurrentPage > 1;
    public ICollection<T> Items { get; set; } = new List<T>();
}