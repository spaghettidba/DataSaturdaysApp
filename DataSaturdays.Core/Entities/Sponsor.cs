using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Entities
{
    public class Sponsor
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string? ImageURL { get; set; }
        public byte[]? Image { get; set; }
        public string? LinkURL { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? MarginTop { get; set; }
        public int? MarginBottom { get; set;}
        public string? Level { get; set; }
        public bool ToDelete { get; set; } = false;
    }
}
