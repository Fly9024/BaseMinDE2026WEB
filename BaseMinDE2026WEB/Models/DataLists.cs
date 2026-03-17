using Microsoft.EntityFrameworkCore;

namespace BaseMinDE2026WEB.Models
{    
    //этот файл не будет изменяться при выполнении scaffold
    //частичный класс, в котором содержатся данные для вставки в базу при применении миграции. 
    //для запуска миграции нужно:
    //1. в классе DeWeb2026Context в методе OnConfiguring прописать правильные параметры подключения к вашей СУБД
    //2. открыть в терминале папку, содержащую ваш файл проекта (.csproj)
    //3. написать dotnet ef database update  
    public partial class DeWeb2026Context : DbContext
    {
        //заполняются все справочники тестовыми данными, которые должны быть по заданию
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentTypeTable>().HasData(
                new PaymentTypeTable { Name = "наличные", IdPaymentType =1},
                new PaymentTypeTable { Name = "перевод" , IdPaymentType =2});

            modelBuilder.Entity<CourceTable>().HasData(
                new CourceTable { Name = "Основы веб-дизайна", IdCource =1},
                new CourceTable { Name = "Основы алгоритмизации и программирования", IdCource=2},
                new CourceTable { Name = "Основы проектирования баз данных", IdCource=3});

            modelBuilder.Entity<OrderStatusTable>().HasData(
                new OrderStatusTable { Name = "Новая", IdOrderStatus=1},
                new OrderStatusTable { Name = "Идет обучение", IdOrderStatus =2},
                new OrderStatusTable { Name = "Обучение завершено", IdOrderStatus =3}
                );
            modelBuilder.Entity<LoginTable>().HasData(
                new LoginTable { Login = "Admin", Password="KorokNET", IsAdmin = true });
        }
    }
}
