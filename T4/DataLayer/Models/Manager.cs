﻿using System;
using System.Collections.Generic;

namespace T4.DataLayer.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public DateTime LastUpdate { get; set; }
        

        public Manager(string lastName)
        {
            LastName = lastName;
            LastUpdate = DateTime.MinValue;
        }
    }
}