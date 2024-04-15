using System;
using System.Collections.Generic;

namespace Long_Term_Care.Models;

public partial class Physical
{
    public int PhysicalId { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public byte? Sbp { get; set; }

    public byte? Dbp { get; set; }

    public decimal? Waist { get; set; }

    public decimal? Wbc { get; set; }

    public decimal? Rbc { get; set; }

    public decimal? Hb { get; set; }

    public decimal? Hct { get; set; }

    public int? Plt { get; set; }

    public decimal? Bun { get; set; }

    public decimal? Crea { get; set; }

    public decimal? Tg { get; set; }

    public decimal? Hdl { get; set; }

    public decimal? Ldl { get; set; }

    public decimal? Tcho { get; set; }

    public decimal? Ast { get; set; }

    public decimal? Alt { get; set; }

    public decimal? Uibc { get; set; }

    public decimal? Fe { get; set; }

    public int? CaseId { get; set; }

    public int? MemberId { get; set; }

    public virtual Case? Case { get; set; }

    public virtual Member? Member { get; set; }
}
