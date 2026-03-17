using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BaseMinDE2026WEB.Models;

namespace BaseMinDE2026WEB.Pages
{
    public class RegistrationPageModel(DeWeb2026Context db) : PageModel
    {
        [BindProperty]//чтобы мы могли сделать привязку
        //веделяем память не только под логин, но под объект с данными о пользователе
        public LoginTable NewUser { get; set; } 
            = new LoginTable() { UserTable = new UserTable() };

        public void OnGet()
        {
        }

        //это обработчик запроса на регистрацию со страницы. все поля уже прошли валидацию и поместились с помощью привязок в объект с новым пользователем
        public IActionResult OnPost() 
        {
            db.LoginTables.Add(NewUser);
            db.SaveChanges();
            return RedirectToPage("/Index");        
        }
        public IActionResult OnGetLoginValidate(string login)//обработчик запроса на получение логина
            => Content(db.LoginTables //возвращаем текст
                .Select(x => x.Login) //делаем проекцию к столбцу с логином
                .Contains(login) //проверяем, содержится ли переданный логин в столбце с логинами в базе
                .ToString()); //переводим логическое значение в текст
    }
}
