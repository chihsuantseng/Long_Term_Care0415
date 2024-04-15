using System;
using System.Collections.Generic;

namespace Long_Term_Care.Models;

public partial class AppointmentForm
{
    public int ReserveId { get; set; }

    public int MemberId { get; set; }

    public DateOnly? ReserveTime { get; set; }

    public int CaseId { get; set; }

    public byte[]? CaseAvatar { get; set; } 

    public string CaseName { get; set; } = null!;

    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string? ServiceId { get; set; }

    public DateOnly? WorkingDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual ICollection<CareRecord> CareRecords { get; set; } = new List<CareRecord>();

    public virtual Case? Case { get; set; } 

    public virtual Employee? Employee { get; set; } 

    public virtual Member? Member { get; set; } 

    public virtual ServicesItem? Service { get; set; }
}
