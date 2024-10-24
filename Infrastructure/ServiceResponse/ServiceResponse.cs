using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceResponse
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = null;
        public string? Error { get; set; } = null;
        public string? Hint { get; set; } = null;
        public double? PriceTotal { get; set; } = null;
        public List<string>? ErrorMessages { get; set; } = null;
        public string RefreshToken { get; set; }
    }

    public class PaginationModel<T>
    {
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecords { get; set; }
        public List<T> ListData { get; set; }
    }

    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; init; }
        public int TotalPages { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PaginatedList(List<T> items,int count,int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages= (int)Math.Ceiling(count/(double)pageSize);
            AddRange(items);
        }

        public static async Task<PaginatedList<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1)*pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);    
        }
    }
}
