using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Entities
{
    public class Preconference
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? RegistrationUrl { get; set; }

        public List<Speaker>? Speakers { get; set; } = new(); // max 4

        public bool ToDelete { get; set; } = false;
    }
}
