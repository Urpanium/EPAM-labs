using System;
using System.Collections.Generic;

namespace T5.Models
{

    public class PageIndexViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int ItemsCount { get; set; }
        public int PagesCount => (int)Math.Ceiling((decimal)ItemsCount / PageSize);
    }
}