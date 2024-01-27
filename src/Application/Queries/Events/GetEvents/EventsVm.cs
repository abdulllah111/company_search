using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Queries.Events.GetEvents
{
    public class EventsVm
    {
        public IList<EventDto>? Events { get; set; }
    }
}