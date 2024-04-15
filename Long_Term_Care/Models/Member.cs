using System;
using System.Collections.Generic;

namespace Long_Term_Care.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[]? MemberAvatar { get; set; }

    public string MemberName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string? Email { get; set; }

    public string? HomePhone { get; set; }

    public string MobilePhone { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? CaseId { get; set; }

    public virtual ICollection<AppointmentForm> AppointmentForms { get; set; } = new List<AppointmentForm>();

    public virtual ICollection<Bservice> Bservices { get; set; } = new List<Bservice>();

    public virtual ICollection<CareRecord> CareRecords { get; set; } = new List<CareRecord>();

    public virtual Case? Case { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();

    public virtual ICollection<Gservice> Gservices { get; set; } = new List<Gservice>();
}
