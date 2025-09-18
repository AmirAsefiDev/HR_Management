using Microsoft.EntityFrameworkCore;

namespace HR_Management.Common.Pagination;

public static class Pagination
{
    //executes in DB with IQueryable
    public static async Task<(List<TSource> items, int totalPage)> ToPagedAsync<TSource>(
        this IQueryable<TSource> query, int page, int pageSize)
    {
        var count = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(count / (double)pageSize);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, totalPage);
    }

    //executes in memory of project
    public static Task<(List<TSource> items, int totalPage)> ToPaged<TSource>(
        this IEnumerable<TSource> source, int page, int pageSize)
    {
        var count = source.Count();
        var totalPage = (int)Math.Ceiling(count / (double)pageSize);
        var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return Task.FromResult((items, totalPage));
    }
}