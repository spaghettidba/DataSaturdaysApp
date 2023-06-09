﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Entities
{
    public class Milestone
    {
        public Guid Id { get; set; } 
        public int Order { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string? DateText { get; set; }
    }
}
