namespace HR_Management.Common.Pagination;

public class PagedResultDto<T>
{
    public List<T> Items { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int PageSize { get; set; }

    public List<ResponseFilterDto> Filter { get; set; }
}