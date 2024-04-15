using System;
using System.Collections.Generic;

namespace Long_Term_Care.Models;

public partial class Case
{
    public int CaseId { get; set; }

    public string CaseName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public int Age { get; set; }

    public string IdentificationNumber { get; set; } = null!;

    public string IdentityType { get; set; } = null!;

    public string? CasePhone { get; set; }

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string FamilyName { get; set; } = null!;

    public string FamilyPhone { get; set; } = null!;

    public string Relation { get; set; } = null!;

    public byte[]? CaseAvatar { get; set; }

    public int? MemberId { get; set; }

    public virtual ICollection<AppointmentForm> AppointmentForms { get; set; } = new List<AppointmentForm>();

    public virtual ICollection<CareRecord> CareRecords { get; set; } = new List<CareRecord>();

    public virtual Member? Member { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
