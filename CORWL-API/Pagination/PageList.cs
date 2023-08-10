﻿
using Microsoft.EntityFrameworkCore;

namespace CORWL_API.Pagination
{
    public class PageList<T> : List<T>
    {
        public PageList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PageList<T>> CreateAsynch(IQueryable<T> source, int pageNumber, int pageSize)
        {
            //countAsynch will make database call
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}
