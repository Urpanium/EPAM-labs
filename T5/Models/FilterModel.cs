﻿using System;

namespace T5.Models
{
    public class FilterModel
    {
        public string ManagerLastName { get; set; }
        
        public string FromDateString { get; set; }
        
        public string ToDateString { get; set; }
        public int PageNumber { get; set; }

        public override string ToString()
        {
            return $"{ManagerLastName} {FromDateString} {ToDateString} {PageNumber}";
        }
    }
}