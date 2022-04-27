namespace Shared.RequestFeatures;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }

    private PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData()
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPage = (int)Math.Ceiling(count / (double)pageSize)
        };

        AddRange(items);
    }

    
     public static PagedList<T> ToPagedList(IEnumerable<T> source, int fullCount, int pageNumber, int pageSize) 
        => new PagedList<T>(source.ToList(), fullCount, pageNumber, pageSize);
    
}
