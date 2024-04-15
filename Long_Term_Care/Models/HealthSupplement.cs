using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Long_Term_Care.Models;

public partial class HealthSupplement
{
	[Key]
	[DisplayName("產品編號")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int SupplementId { get; set; }

	[Required]
	[DisplayName("商品名稱")]
	public string SupplementName { get; set; } = null!;

	[Required]
	[Display(Name = "商品介紹")]
	public string SupplementDescription { get; set; } = null!;

	[Required]
	[Display(Name = "適用族群")]
	public string SupplementEthnic { get; set; } = null!;

	[Required]
	[Display(Name = "注意事項")]
	public string SupplementPrecautions { get; set; } = null!;

	[Required]
	[DisplayName("單價")]
	[Range(1, 2000)]
	public double Supplement_Price { get; set; }

	[Required]
	[DisplayName("5-9個價格")]
	[Range(1, 2000)]
	public double Supplement_MidTermPrice5 { get; set; }

	[Required]
	[DisplayName("10+個價格")]
	[Range(1, 2000)]
	public double Supplement_LongTermPrice10 { get; set; }

	[DisplayName("商品圖片")]
	[ValidateNever]
	public byte[]? SupplementImg { get; set; }

}
