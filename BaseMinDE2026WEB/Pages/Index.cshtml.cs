using BaseMinDE2026WEB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BaseMinDE2026WEB.Pages
{
    public class IndexModel(DeWeb2026Context db) : PageModel
    {
        [BindProperty] public string Message {  get; set; }
        public void OnGet(string message)
        {
            Message = message;
        }

        public async Task<IActionResult> OnPost(string login,string password) 
        {
            LoginTable? current = db.LoginTables.FirstOrDefault(
                x=>x.Login==login && x.Password==password);
            if (current == null)
            {
                return RedirectToPage("/Index", new { message = "¬ведены некорректные или несуществующие данные" });
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, current.Login),
                    new Claim(ClaimTypes.Role, current.IsAdmin?"Admin":"User")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddSeconds(60)
                };
                await HttpContext.SignInAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme,
                 new ClaimsPrincipal(claimsIdentity),
                 authProperties);

                return current.IsAdmin? RedirectToPage("/AdminPage"):RedirectToPage("/UserPage");
            }
        }
        async public Task<IActionResult> OnPostExit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }

    }
}
