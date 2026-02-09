using BaseMinDE2026WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaseMinDE2026WEB.Pages
{
    [Authorize]
    public class AddOrderPageModel(DeWeb2026Context db) : PageModel
    {
        public List<PaymentTypeTable> PaymentTypes => db.PaymentTypeTables.ToList();
        public List<CourceTable> Cources => db.CourceTables.ToList();


        [BindProperty] 
        public OrderTable NewOrder {  get; set; } = new OrderTable();
        public void OnGet()
        {
        }

        public void OnPost()
        {
            NewOrder.User = User.Identity.Name;
            db.OrderTables.Add(NewOrder);
            db.SaveChanges();
        }
    }
}
