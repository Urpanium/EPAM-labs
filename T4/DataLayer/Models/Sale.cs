﻿using System;

namespace T4.DataLayer.Models
{
    public class Sale
    {
        public int Id { get; set; }
        
        public virtual Client Client { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Product Product { get; set; }
        
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"{Id}, Client {Client?.Id}, Manager {Manager?.Id}, {DateTime.Date}";
        }
    }
}