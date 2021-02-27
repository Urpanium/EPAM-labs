using System;

namespace T5.Service
{
    public class FilterModel
    {
        public string ManagerLastName { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public int PageNumber { get; set; }
    }
}