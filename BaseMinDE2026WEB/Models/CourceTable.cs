using System;
using System.Collections.Generic;

namespace BaseMinDE2026WEB.Models;

public partial class CourceTable
{
    public int IdCource { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
}
