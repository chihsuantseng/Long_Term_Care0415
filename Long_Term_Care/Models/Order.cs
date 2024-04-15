using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Long_Term_Care.Models;

public partial class Order
{
	public int OrderId { get; set; }

	[DisplayName("訂單編號")]
	public string? OrderGuid { get; set; }

	[DisplayName("會員帳號")]
	public string? UserName { get; set; }

	[DisplayName("收件人姓名")]
	public string? Receiver { get; set; }

	[DisplayName("收件人信箱")]
	public string? Email { get; set; }

	[DisplayName("城市")]
	public string? City { get; set; }

	[DisplayName("區域")]
	public string? District { get; set; }

	[DisplayName("地址")]
	public string? Address { get; set; }

	[DisplayName("訂單日期")]
	public DateTime? Date { get; set; }

	[DisplayName("訂單狀態")]
	public string? Status { get; set; }

	[DisplayName("總金額")]
	public int? TotalPrice { get; set; }
}
