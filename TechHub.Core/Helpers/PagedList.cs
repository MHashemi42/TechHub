namespace TechHub.Core.Helpers;

public class PagedList<T> : List<T>
{
    public PagedListMetaData MetaData { get; init; }

    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new PagedListMetaData
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };

        AddRange(items);
    }

    public PagedList(IEnumerable<T> items, PagedListMetaData metaData)
    {
        MetaData = metaData;
        AddRange(items);
    }
}
