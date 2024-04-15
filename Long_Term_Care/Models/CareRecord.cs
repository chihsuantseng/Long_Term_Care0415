using System;
using System.Collections.Generic;

namespace Long_Term_Care.Models;

public partial class CareRecord
{
    public int RecordId { get; set; }

    public int? ReserveId { get; set; }

    public int? MemberId { get; set; }

    public int? CaseId { get; set; }

    public string? CaseName { get; set; }

    public int? EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public DateOnly? RecordDate { get; set; }

    public TimeOnly? RecordTime { get; set; }

    public decimal? Temperature { get; set; }

    public byte? Heartbeat { get; set; }

    public byte? Breathe { get; set; }

    public short? BefGlucose { get; set; }

    public short? AftGlucose { get; set; }

    public short? Sbp { get; set; }

    public short? Dbp { get; set; }

    public string? Memo { get; set; }

    public virtual Case? Case { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Member? Member { get; set; }

    public virtual AppointmentForm? Reserve { get; set; }
}
