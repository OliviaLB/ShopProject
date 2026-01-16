namespace Contracts;

public class PagedList<T>
{
    public List<T> Items { get; set; } = [];
    public Pagination Pagination { get; set; } = new Pagination();

    public PagedList() { }

    public PagedList(List<T> items, int pageNumber, int pageSize, int totalItems)
    {
        Items = items;
        Pagination = new Pagination(pageNumber, pageSize, totalItems);
    }
}