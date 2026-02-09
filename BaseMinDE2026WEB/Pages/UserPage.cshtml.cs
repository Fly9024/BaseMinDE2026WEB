using BaseMinDE2026WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseMinDE2026WEB.Pages
{
    [Authorize]
    public class UserPageModel(DeWeb2026Context db) : PageModel
    {
        public List<OrderTable> UserOrders =>db.OrderTables
            .Include(x=>x.IdCourseNavigation)
            .Include(x=>x.IdPaymentTypeNavigation)
            .Include(x=>x.IdStatusNavigation)
            .Where(x=>x.User == User.Identity.Name)
            .ToList();
        public void OnGet()
        {
        }
    }
}
