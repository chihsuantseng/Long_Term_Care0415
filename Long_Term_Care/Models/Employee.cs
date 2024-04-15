using System;
using System.Collections.Generic;

namespace Long_Term_Care.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[]? EmployeeAvatar { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public int Age { get; set; }

    public string IdentificationNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string? HomePhone { get; set; }

    public string MobilePhone { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public string EmploymentStatus { get; set; } = null!;

    public string? Supervisor { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<AppointmentForm> AppointmentForms { get; set; } = new List<AppointmentForm>();

    public virtual ICollection<CareRecord> CareRecords { get; set; } = new List<CareRecord>();
}
