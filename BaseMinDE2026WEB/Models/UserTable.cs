using System;
using System.Collections.Generic;

namespace BaseMinDE2026WEB.Models;

public partial class UserTable
{
    public string Login { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public virtual LoginTable LoginNavigation { get; set; } = null!;

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
}
