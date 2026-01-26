using System;
using System.Collections.Generic;

namespace BaseMinDE2026WEB.Models;

public partial class PaymentTypeTable
{
    public int IdPaymentType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
}
