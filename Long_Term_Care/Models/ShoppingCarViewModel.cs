using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Long_Term_Care.Models
{
	public class ShoppingCarViewModel
	{
		public IEnumerable<OrderDetail> OrderDetail { get; set; }
		public IEnumerable<HealthSupplement> HealthSupplement { get; set; }
	}
}
