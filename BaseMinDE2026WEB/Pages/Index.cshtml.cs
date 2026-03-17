using BaseMinDE2026WEB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BaseMinDE2026WEB.Pages
{
    //добавляем подключение к БД в основной конструктор через внедрение зависимостей (на других страницах так же)
    public class IndexModel(DeWeb2026Context db) : PageModel
    {
        //сообщение для пользователя, если логин или пароль не подходит
        //в данном случае оно работает через перезагрузку страницы
        [BindProperty] public string Message {  get; set; }
        public void OnGet(string message)
        {
            Message = message;
        }
        //обработчик post-запроса на авторизацию. внутри проверяются логин и пароль, при успешном входе устанавливаются куки
        public async Task<IActionResult> OnPost(string login,string password) 
        {
            //ищем в базе пользователя с таким логином и паролем
            LoginTable? current = db.LoginTables.FirstOrDefault(
                x=>x.Login==login && x.Password==password);
            if (current == null)//если такого пользователя нет, перезагружаем страницу с соответствующим сообщением
            {
                return RedirectToPage("/Index", new { message = "Введены некорректные или несуществующие данные" });
            }
            else//если пользователь существует
            {
                var claims = new List<Claim> //определяем список проверяемых элементов при авторизации
                {//в нашем случае это роль и логин пользователя
                    new Claim(ClaimTypes.Name, current.Login),
                    new Claim(ClaimTypes.Role, current.IsAdmin?"Admin":"User")
                };
                //отождествляем эти требования со схемой аутентификации через куки
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //задаем параметры действия куки
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, //сохраняются между сессиями (например, при перезагрузке браузера)
                    ExpiresUtc = DateTime.UtcNow.AddSeconds(600) //время существования куки-файла
                };
                //вызываем системный метод авторизации
                await HttpContext.SignInAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme,
                 new ClaimsPrincipal(claimsIdentity),
                 authProperties);
                //переходим на соответсвующую страницу в зависимости от роли
                return current.IsAdmin? RedirectToPage("/AdminPage"):RedirectToPage("/UserPage");
            }
        }
        //метод выхода из системы. Вызывается по нажатию на кнопку выход, которая у нас написана в файле _Layout.cshtml (чтобы была видна на всех страницах)
        async public Task<IActionResult> OnPostExit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }

    }
}
