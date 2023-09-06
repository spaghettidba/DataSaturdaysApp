using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; }    
        public Uri JoinURL { get; set; }
        public bool ToDelete { get; set; } = false;
    }
}
