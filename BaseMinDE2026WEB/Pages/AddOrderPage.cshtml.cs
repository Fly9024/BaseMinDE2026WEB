using BaseMinDE2026WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaseMinDE2026WEB.Pages
{
    [Authorize] //страница доступна только для авторизованного пользователя
    public class AddOrderPageModel(DeWeb2026Context db) : PageModel
    {
        //необходимая информация в раскрывающиеся списки
        public List<PaymentTypeTable> PaymentTypes => db.PaymentTypeTables.ToList();
        public List<CourceTable> Cources => db.CourceTables.ToList();

        [BindProperty] //сама заявка, к которой будут привязки данных
        public OrderTable NewOrder {  get; set; } = new OrderTable();
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            NewOrder.User = User.Identity.Name;//логин пользователя подставляем через его Identity
            db.OrderTables.Add(NewOrder);//остальное считываем с полей на странице
            db.SaveChanges();
            return RedirectToPage("/UserPage");
        }
    }
}
