using System;
using System.Collections.Generic;

namespace Long_Term_Care.Models;

public partial class Bservice
{
	public int BplanId { get; set; }

	public int? MemberId { get; set; }

	public int? CaseId { get; set; }

	public DateOnly? Bdate { get; set; }

	public int? Addtotal { get; set; }

	public int? Balnum { get; set; }

	public int? Ownexp { get; set; }

	public string? CmsRanged { get; set; }

	public int? Cms { get; set; }

	public virtual Case? Case { get; set; }

	public virtual Member? Member { get; set; }
}
