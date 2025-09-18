namespace HR_Management.Common.Pagination;

public class PaginationDto
{
    public int pageNumber { get; set; } = 1;
    public int pageSize { get; set; } = 10;
    public string? filter { get; set; }
    public string? searchKey { get; set; }
    public string? sortType { get; set; }
    public bool? isDescending { get; set; } = true;
}