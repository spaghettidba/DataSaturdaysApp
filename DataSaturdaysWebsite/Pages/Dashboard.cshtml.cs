using DataSaturdays.Core.Entities;
using DataSaturdays.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataSaturdaysWebsite.Pages
{
    public class UserModel : PageModel
    {
        private readonly ILogger<UserModel> _logger;

        private readonly IEventService _eventService;
        public IEnumerable<Event> Events { get; set; }

        public UserModel(ILogger<UserModel> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }
    }
}