using System;
using System.Collections.Generic;

namespace BaseMinDE2026WEB.Models;

public partial class LoginTable
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public virtual UserTable? UserTable { get; set; }
}
