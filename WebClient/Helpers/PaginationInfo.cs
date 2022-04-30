namespace WebClient.Helpers;

public class PaginationInfo
{
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }


    public PaginationInfo(int totalCount, int totalPage, int currentPage, int pageSize,
         bool hasPrevious, bool hasNext)
    {
        this.TotalCount = totalCount;
        this.TotalPage = totalPage;
        this.CurrentPage = currentPage;
        this.PageSize = pageSize;
        this.HasNext = hasNext;
        this.HasPrevious = hasPrevious;
    }
}
