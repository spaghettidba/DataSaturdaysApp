using DataSaturdays.Core.Entities;
using DataSaturdays.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataSaturdaysWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IEventService _eventService;
        public IEnumerable<Event> Events{ get; set; }

        public IndexModel(ILogger<IndexModel> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        public async Task OnGet()
        {
            Events = await _eventService.GetEventsAsync();
        }
    }
}