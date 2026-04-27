namespace api.Helpers;

public class PagedResult<T, R>(int pageNumber, int pageSize, int items, IReadOnlyList<R> data) where T : class
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int Items { get; set; } = items;
    public IReadOnlyList<R> Data { get; set; } = data;
}
