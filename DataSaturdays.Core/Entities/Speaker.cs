﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Entities
{
    public class Speaker
    {
        public Guid Id { get; set; }
        public Guid PreconId { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Profile { get; set; }
        public bool ToDelete { get; set; } = false;
    }
}
