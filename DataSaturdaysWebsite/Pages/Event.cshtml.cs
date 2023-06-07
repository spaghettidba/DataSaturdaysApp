using DataSaturdays.Core.Entities;
using DataSaturdays.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataSaturdaysWebsite.Pages
{
    public class EventModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IEventService _eventService;
        public Event Event{ get; set; }
        
        
        public EventModel(ILogger<IndexModel> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        public async Task OnGet(Guid EventId)
        {
            Event = await _eventService.GetEventByIdAsync(EventId);
        }
    }
}