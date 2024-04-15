using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Long_Term_Care.Models;

public partial class OrderDetail
{
	public int OrderDetailId { get; set; }

	[DisplayName("訂單編號")]
	public string? OrderGuid { get; set; }

	[DisplayName("會員帳號")]
	public string? UserName { get; set; }

	[DisplayName("產品編號")]
	public int SupplementId { get; set; }

	[DisplayName("產品名稱")]
	public string? SupplementName { get; set; }

	[DisplayName("單價")]
	public int? SupplementPrice { get; set; }

	[DisplayName("訂購數量")]
	public int? Quantity { get; set; }

	[DisplayName("是否形成訂單")]
	public string? IsApproved { get; set; }
}

