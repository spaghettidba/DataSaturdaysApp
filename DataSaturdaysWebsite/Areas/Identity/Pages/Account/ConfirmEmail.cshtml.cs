using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataSaturdaysWebsite.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ConfirmEmailModel> _logger;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, ILogger<ConfirmEmailModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return NotFound("Invalid confirmation link.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = System.Web.HttpUtility.UrlDecode(code);

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                _logger.LogInformation("User confirmed email for ID '{UserId}'.", userId);
                StatusMessage = "Thank you for confirming your email.";
                return Page();
            }
            else
            {
                StatusMessage = "Error confirming your email.";
                return Page();
            }
        }
    }
}
