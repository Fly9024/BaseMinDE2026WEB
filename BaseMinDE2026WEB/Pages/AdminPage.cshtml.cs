using BaseMinDE2026WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BaseMinDE2026WEB.Pages
{
    [Authorize(Roles ="Admin")]
    public class AdminPageModel(DeWeb2026Context db) : PageModel
    {
        public int NPages {  get; set; }//количество страниц для пагинации
        
        public List<OrderStatusTable> OrderStatuses => db.OrderStatusTables.ToList();
        
        //получаем список со статусами для фильтрации путем добавления пункта со всеми статусами к предыдущему списку
        public List<OrderStatusTable> FilterStatuses => 
            [new OrderStatusTable(){ Name ="Все статусы", IdOrderStatus=0 },
            ..OrderStatuses];

       //список получается сразу со всеми сортировками и фильтрами, если они установлены
        public List<OrderTable> UserOrders { get; set; }
        
        //для фильтрации используем тот же метод OnGet. Если параметры сортировок и фильтров не применены, то они будут игнорироваться в методе
        public void OnGet(string textFind, int dateOrder, int statusFilter, int countOnPage,int nPage)
        {
            

            UserOrders = db.OrderTables
           .Include(x => x.IdCourseNavigation)
           .Include(x => x.IdPaymentTypeNavigation)
           .Include(x => x.IdStatusNavigation)           
           .ToList();

            //сортировка по дате
            UserOrders = dateOrder == 2 ?
                UserOrders.OrderBy(x => x.StartDate).ToList() :
                UserOrders.OrderByDescending(x => x.StartDate).ToList();

            //фильтр по тексту, если он не пустой
            if(!string.IsNullOrEmpty(textFind))
            {
                UserOrders = UserOrders.Where(x=>x.User.StartsWith(textFind)).ToList();
            }
            //фильтр по статусу, если он выбран
            if (statusFilter != 0)
            {
                UserOrders = UserOrders.Where(x=>x.IdStatus == statusFilter).ToList();
            }

            NPages =countOnPage==0?1: UserOrders.Count/countOnPage;//это не совсем верная формула для подсчета количества странц, зато быстро

            //отображение на странице
            if (countOnPage != 0)
            {
                UserOrders = UserOrders.Skip(countOnPage* nPage).Take(countOnPage).ToList();
            }


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
