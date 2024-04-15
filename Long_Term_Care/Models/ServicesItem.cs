using System;
using System.Collections.Generic;

namespace Long_Term_Care.Models;

public partial class ServicesItem
{
    public string ServiceId { get; set; } = null!;

    public string? ServiceName { get; set; }

    public int? ServicePrice { get; set; }

    public virtual ICollection<AppointmentForm> AppointmentForms { get; set; } = new List<AppointmentForm>();
}
