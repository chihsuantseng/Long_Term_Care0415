using Long_Term_Care.Models.health;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace Long_Term_Care.Controllers.Health
{
	public class HealthFormController : Controller
	{
		public IActionResult health_form()
		{
			return View();
		}

		[HttpPost]
		public IActionResult health_form(Health_forms_text model)
		{
			if (ModelState.IsValid)
			{
				var products = InitializeProducts();
				var bmi = CalculateBMI(model.Height, model.Weight);
				var productData = GetProductDataByBMI(bmi);
				products = ProcessForm(products);
				UpdateProducts(products, productData);
				var topThree = GetTopThreeProducts(products);
				var name = model.Name;
				StoreSessionData(name, topThree);

				Console.WriteLine("輸出文字:form");
				foreach (var product in topThree)
				{
					Console.WriteLine(product);
				}
				Console.WriteLine(name);

				return RedirectToAction("health_answer");
			}
			else
			{
				return View(model);
			}
		}
		private Dictionary<string, int> InitializeProducts()
		{
			return new Dictionary<string, int>
		{
			{ "產品C", 0 },
			{ "產品鐵", 0 },
			{ "產品B", 0 },
			{ "產品UCII", 0 },
			{ "產品鈣", 0 },
			{ "產品魚油", 0 },
			{ "產品D", 0 },
			{ "產品紅麴", 0 },
			{ "產品益生菌", 0 },
			{ "產品酵素", 0 }
		};
		}
		private double CalculateBMI(double height, double weight)
		{
			return weight / ((height / 100) * (height / 100));
		}

		private KeyValuePair<string, string> GetProductDataByBMI(double bmi)
		{
			if (bmi < 18.5)
			{
				return new KeyValuePair<string, string>("30", "產品鈣");
			}
			else if (bmi > 24)
			{
				return new KeyValuePair<string, string>("30", "產品紅麴");
			}
			else
			{
				return new KeyValuePair<string, string>("1", "產品C");
			}
		}
		private Dictionary<string, int> ProcessForm(Dictionary<string, int> products)
		{
			for (int i = 1; i <= 17; i++)
			{
				// 挑出name="qi"的表單回傳選項，依據','切割前後放入parts[]
				string selectedValue = Request.Form["q" + i];
				string[] parts = selectedValue.Split(',');

				for (int j = 0; j < parts.Length; j += 2)
				{
					string numericPart = parts[j];
					string productInfo = parts[j + 1];

					Console.WriteLine("parts.Length數量: " + parts.Length);
					Console.WriteLine("第" + i + "題: " + numericPart);
					Console.WriteLine("第" + i + "題: " + productInfo);

					// 如果選擇了某個產品
					if (!string.IsNullOrEmpty(productInfo))
					{
						// 將 numericPart 轉換為整數
						if (int.TryParse(numericPart, out int value))
						{
							// 如果存在，則更新現有鍵的值
							if (products.ContainsKey(productInfo))
							{
								products[productInfo] += value;
							}
							// 否則創建一個新的鍵值對
							else
							{
								products.Add(productInfo, value);
							}
						}
					}
				}
			}
			return products;
		}
		private void UpdateProducts(Dictionary<string, int> products, KeyValuePair<string, string> productData)
		{
			products[productData.Value] += int.Parse(productData.Key);
		}

		private List<string> GetTopThreeProducts(Dictionary<string, int> products)
		{
			return products.OrderByDescending(p => p.Value).Select(p => p.Key).Take(3).ToList();
		}

		private void StoreSessionData(string name, List<string> topThree)
		{
			if (name == null)
			{
				name = "用戶";
			}
			if (topThree == null)
			{
				topThree = new List<string> { "產品C", "產品B", "產品UCII" };
			}
			// 將 name 存儲到 Session 中
			HttpContext.Session.SetString("UserName", name);

			// 將 topThree 轉換為 JSON 字串
			string topThreeJson = JsonConvert.SerializeObject(topThree);

			// 將 JSON 字串轉換為 byte[]
			byte[] topThreeBytes = Encoding.UTF8.GetBytes(topThreeJson);

			// 將 topThreeBytes 存儲到 Session 中
			HttpContext.Session.Set("TopThreeProducts", topThreeBytes);
		}

		public IActionResult health_answer(Health_forms_text model)
		{
			var u_name = HttpContext.Session.GetString("UserName");
			var topThreeJson = HttpContext.Session.GetString("TopThreeProducts");
			var topthree_answer = JsonConvert.DeserializeObject<List<string>>(topThreeJson!);

			var product_detail = new Dictionary<string, (string name_detail, string image_source, string SupplementId, string Description)>
			{
				{ "產品C", ("西印度櫻桃萃取維他命C", "health_item_1.png","1","●95%專利C3薑黃完整保留活性"+"\n\n●台灣專利牛樟芝子實體(非菌絲體)"+"\n\n●超過120項實驗功效專利，高效率幫助循環代謝"+"\n\n●美國專利95%黑胡椒鹼:強化生物利用率") },
				{ "產品鐵", ("天然酵母維生素B群+Q10+專利鐵", "health_item_2.png","2","●3大美妍成分:Q10+專利鐵+維生素C"+"\n\n●天然酵母B群:朝氣活力，生物利用率高"+"\n\n●完整8種B群，滿足一日所需"+"\n\n●美國專利95%黑胡椒鹼:強化生物利用率"+"\n\n●穩定神經系統與正常代謝") },
				{ "產品B", ("天然酵母維生素B群+頂級黑瑪卡+鋅酵母", "health_item_3.png","3","●天然酵母B群:朝氣活力，生物利用率高"+"\n\n●祕魯黑瑪卡:增強體力"+"\n\n●天然鋅酵母:滋補強身"+"\n\n●美國專利95%黑胡椒鹼:強化生物利用率"+"\n\n●完整8種B群，滿足一日所需"+"全素可食") },
				{ "產品UCII", ("專利UC2+高效葡萄糖胺+專利C3薑黃", "health_item_4.png","4","●針對登山、靈活行動力所開發"+"\n\n●足量添加葡萄糖胺460mg、UC-II 42mg"+"\n\n●低溫專利萃取UC-II，完整保留膠原蛋白活性及功效"+"\n\n●薑黃+黑胡椒:提升薑黃利用率，調節生理機能") },
				{ "產品鈣", ("高比例海藻鈣+海洋鎂+維生素D+K2", "health_item_5.png","5","●採集自愛爾蘭高比例海藻鈣，100%天然植物素食鈣"+"\n\n●具有鎂、鋅、鐵、碘等70種以上法國天然海洋礦物質"+"\n\n●添加歐洲天然酵母維生素D2、鷹嘴豆發酵萃取維生素K2，補鈣更鎖鈣"+"\n\n●維持鈣離子平衡，穩定情緒"+"\n\n●全素可食") },
				{ "產品魚油", ("85% rTG 高濃度魚油 Omega-3", "health_item_7.png","7","●rTG型態，高吸收率"+"\n\n●挪威小型純淨魚種，先進短程蒸餾技術"+"\n\n●獨家比例！EPA：DHA = 550mg：250mg"+"\n\n●Omega3 實測高含量達 91.98%，高 EPA 幫助代謝") },
				{ "產品D", ("天然酵母維生素D", "health_item_8.png","8","●補充陽光能量"+"\n\n●兩粒補充每日所需"+"\n\n●增進鈣質吸收，維持肌肉神經正常機能"+"●非活性維生素D2來自天然酵母且無化學添加"+"\n\n●原料來自歐洲Lalmin酵母大廠") },
				{ "產品紅麴", ("專利紅麴+Q10+天然蕎麥B12", "health_item_9.png","9","●日本專利紅麴:保養暢通，有助降低血中總膽固醇(功效由學理得知)"+"\n\n義大利活性葉酸:直接利用，加強B12維持機能平衡"+"\n\n●日本專利還原型Q10、奧地利蕎麥來源B12:能量促進"+"\n\n●美國植萃黑胡椒鹼:加強30% Q10 吸收利用") },
				{ "產品益生菌", ("LP28複合300億益生菌+綜合消化酵素", "health_item_10.png","10","●專利技術 SYNPACKR保證2年內維持 300 億菌數"+"\n\n●排便順暢、體調整質雙功效"+"\n\n●黃金四角配方:A菌+B菌+酵素+益生質"+"\n\n●8大優質菌種打造平行體質") },
				{ "產品酵素", ("代謝19種活性超級酵素", "health_item_12.png","12","●原裝美國19種綜合代謝酵素，無添加助瀉劑(番瀉苷、阿勃勒)"+"\n\n●你最需要的6大代謝酵素:分解澱粉、脂肪、纖維、乳糖、蛋白質、葡萄糖"+"\n\n●提高消化率至少60%"+"\n\n●日本專利膳食纖維原料") }
			};

			var productsInfo = topthree_answer!.Select(productName =>
			{
				var (name_detail, image_source, SupplementId, Description) = product_detail[productName];
				return new { p_name = name_detail, img = image_source, id = SupplementId, des = Description };
			}).ToList();

			ViewBag.UserName = u_name;
			ViewBag.ProductsInfo = productsInfo;

			//Console.WriteLine("輸出文字:answer");
			// 輸出 topThree 內容到控制台
			//foreach (var product in productsInfo)
			//{
			//	Console.WriteLine(product);
			//}
			//Console.WriteLine(u_name);

			return View();
		}
	}
}
