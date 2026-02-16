using BaseMinDE2026WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BaseMinDE2026WEB.Pages
{
    [Authorize(Roles ="Admin")]
    public class AdminPageModel(DeWeb2026Context db) : PageModel
    {
        public List<OrderStatusTable> OrderStatuses => db.OrderStatusTables.ToList();

        public List<OrderTable> UserOrders => db.OrderTables
           .Include(x => x.IdCourseNavigation)
           .Include(x => x.IdPaymentTypeNavigation)
           .Include(x => x.IdStatusNavigation)          
           .ToList();
        public void OnGet()
        {
        }

        public void OnGetChangeStatus(int idOrder, int idStatus)
        {
            //находим заявку по известному id
            OrderTable order = db.OrderTables.FirstOrDefault(x => x.IdOrder == idOrder);
            //меняем статус заявки
            order.IdStatus = idStatus;
            //очищаем поле с отзывом при изменении статуса
            order.Reviev = null;         
            //сохраняем изменения в базе
            db.SaveChanges();
        }
    }
}
