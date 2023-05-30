﻿using DataSaturdays.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Data
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();
        Task CreateEvent(Event Input);

    }
}
