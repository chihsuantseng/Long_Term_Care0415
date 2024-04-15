using System;
using System.Collections.Generic;

namespace Long_Term_Care.Models;

public partial class Gservice
{
	public int GplanId { get; set; }

	public int? MemberId { get; set; }

	public int? CaseId { get; set; }

	public DateOnly? Gdate { get; set; }

	public int? Gaddtotal { get; set; }

	public int? Gbalnum { get; set; }

	public int? Gownexp { get; set; }

	public string? GcmsRanged { get; set; }

	public int? Gcms { get; set; }

	public virtual Case? Case { get; set; }

	public virtual Member? Member { get; set; }
}