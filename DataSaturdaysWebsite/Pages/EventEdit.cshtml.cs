using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataSaturdaysWebsite.Pages
{
    public class EventEditModel : PageModel
    {
        private readonly ILogger<UserModel> _logger;

        public Guid Id { get; set; }

        public EventEditModel(ILogger<UserModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(Guid EventId)
        {
            Id = EventId;
        }
    }
}