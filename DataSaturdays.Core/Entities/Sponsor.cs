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
        public string Name { get; set; }
        public Uri? ImageURL { get; set; }
        public byte[] Image { get; set; }
        public Uri? LinkURL { get; set; }
        public int Height { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set;}
    }
}
