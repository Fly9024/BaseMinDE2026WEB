using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaseMinDE2026WEB.Pages
{
    [Authorize]
    public class UserPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
