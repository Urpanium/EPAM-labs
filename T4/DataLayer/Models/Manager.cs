using System;
using System.Collections.Generic;

namespace T4.DataLayer.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        

        public Manager()
        {
            
        }

        public Manager(string lastName)
        {
            LastName = lastName;
        }
    }
}