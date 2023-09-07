using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataSaturdaysWebsite.Pages
{
    public class LoginModel : PageModel
    {
            private readonly ILogger<UserModel> _logger;

            public Guid Id { get; set; }

            public LoginModel(ILogger<UserModel> logger)
            {
                _logger = logger;
            }

            public void OnGet(Guid UserId)
            {
                Id = UserId;
            }
    }
}
