using System;
using System.Collections.Generic;

namespace BaseMinDE2026WEB.Models;

public partial class OrderTable
{
    public int IdOrder { get; set; }

    public DateOnly StartDate { get; set; }

    public int IdCourse { get; set; }

    public int IdPaymentType { get; set; }

    public int IdStatus { get; set; }

    public string? Reviev { get; set; }

    public string User { get; set; } = null!;

    public virtual CourceTable IdCourseNavigation { get; set; } = null!;

    public virtual PaymentTypeTable IdPaymentTypeNavigation { get; set; } = null!;

    public virtual OrderStatusTable IdStatusNavigation { get; set; } = null!;

    public virtual UserTable UserNavigation { get; set; } = null!;
}
