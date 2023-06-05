using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Entities
{
    public class Speaker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Uri? Image { get; set; }
        public Uri? Profile { get; set; }
    }
}
