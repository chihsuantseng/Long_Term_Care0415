using Long_Term_Care.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace Long_Term_Care.Controllers
{
	public class ShoppingController : Controller
	{
		private readonly LongTermCareContext _context;
		private readonly IDistributedCache _cache;
		public ShoppingController(LongTermCareContext context, IDistributedCache cache)
		{
			_context = context;
			_cache = cache;
		}


		//購物車
		public ActionResult ShoppingCar()
		{
			string UserName = User.Identity.Name; // 獲得登入使用者的帳號
	
			// 加入HealthSupplement的model
			var healthSupplements = _context.HealthSupplements.ToList();
			var cachedData = _cache.GetString("MyObject");
			if (cachedData != null)
			{
				// 如果缓存中存在对象，则从缓存中获取并返回
				var myObject = JsonSerializer.Deserialize<List<OrderDetail>>(cachedData);
				if (UserName != null)
				{
					foreach (var item in myObject)
					{
						item.UserName = UserName;

					}
				}
				var viewModel = new ShoppingCarViewModel
				{
					OrderDetail = myObject,
					HealthSupplement = healthSupplements
				};

				return View(viewModel);

				
			}
			

			return View();
		}

		// 加入購物車(保健品一覽表入口)
		public async Task<ActionResult> AddCarAsync(int SupplementId, int Quantity)
		{

			//取得目前通過驗證的使用者名稱
			string UserName = User.Identity.Name;

			//取得該使用者目前購物車內是否已有此商品，且尚未形成訂單的資料

			var cachedData = _cache.GetString("MyObject");
			var HealthSupplement = _context.HealthSupplements.FirstOrDefault(m => m.SupplementId == SupplementId);

			var orderDetail = new OrderDetail();
			var orderDetails = new List<OrderDetail>();

			orderDetail.UserName = UserName;
			orderDetail.SupplementId = HealthSupplement.SupplementId;
			orderDetail.SupplementName = HealthSupplement.SupplementName;
			orderDetail.SupplementPrice = (int?)HealthSupplement.Supplement_Price;
			orderDetail.Quantity = 1;
			orderDetail.IsApproved = "否";
			orderDetails.Add(orderDetail);
			//設定緩存
			if (cachedData == null)
			{
				//如果篩選條件資料為null，代表要新增全新一筆訂單明細資料
				//將產品資料欄位一一對照至訂單明細的欄位



				var serializedObject = JsonSerializer.Serialize(orderDetails);

				_cache.SetString("MyObject", serializedObject, new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // 設置過期時間為十分鐘
				});
				return NoContent();

			}



			var cachedData2 = JsonSerializer.Deserialize<List<OrderDetail>>(cachedData);
			var currentCar = cachedData2.FirstOrDefault(m => m.SupplementId == SupplementId);
			if (currentCar != null)
			{
				var SupplementCount = Quantity;
				currentCar.Quantity += SupplementCount; ;
				if (currentCar.Quantity > 20)
				{
					currentCar.Quantity = 20;
				}
				var serializedObject2 = JsonSerializer.Serialize(cachedData2);
				_cache.SetString("MyObject", serializedObject2, new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // 設置過期時間為十分鐘
				});
				return NoContent();
			}
			cachedData2.Add(orderDetail);


			var serializedObject1 = JsonSerializer.Serialize(cachedData2);
			_cache.SetString("MyObject", serializedObject1, new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // 設置過期時間為十分鐘
			});


			return NoContent();
		}
		// 加入購物車(保健品問卷結果入口)
		public async Task<ActionResult> AddCar_formAsync(int SupplementId, int Quantity)
		{

			//取得目前通過驗證的使用者名稱
			string UserName = User.Identity.Name;

			//取得該使用者目前購物車內是否已有此商品，且尚未形成訂單的資料
			var currentCar = _context.OrderDetails
				.FirstOrDefault(m => m.SupplementId == SupplementId && m.IsApproved == "否" && m.UserName == UserName);
			if (currentCar == null)
			{
				//如果篩選條件資料為null，代表要新增全新一筆訂單明細資料
				//將產品資料欄位一一對照至訂單明細的欄位
				var HealthSupplement = _context.HealthSupplements.FirstOrDefault(m => m.SupplementId == SupplementId);
				var SupplementCount = Quantity;
				var orderDetail = new OrderDetail();
				orderDetail.UserName = UserName;
				orderDetail.SupplementId = HealthSupplement.SupplementId;
				orderDetail.SupplementName = HealthSupplement.SupplementName;
				orderDetail.SupplementPrice = (int?)HealthSupplement.Supplement_Price;
				orderDetail.Quantity = SupplementCount;
				orderDetail.IsApproved = "否";
				_context.OrderDetails.Add(orderDetail);
			}
			else
			{
				var SupplementCount = Quantity;
				currentCar.Quantity += SupplementCount;
				if (currentCar.Quantity > 20)
				{
					currentCar.Quantity = 20;
				}
			}

			//儲存資料庫並導至購物車檢視頁面
			_context.SaveChanges();
			return RedirectToAction("Health_answer", "HealthForm");
		}

		// 增加購物車商品數量
		public ActionResult ItemPlus(int SupplementId)
		{
			string UserName = User.Identity.Name;
			var currentCar = _context.OrderDetails.FirstOrDefault(m => m.SupplementId == SupplementId && m.IsApproved == "否" && m.UserName == UserName);
			var Supplement = _context.HealthSupplements.FirstOrDefault(m => m.SupplementId == SupplementId);
			currentCar.Quantity++;
			if (currentCar.Quantity >= 10)
			{
				currentCar.SupplementPrice = (int?)Supplement.Supplement_LongTermPrice10;
			}
			else if (currentCar.Quantity >= 5)
			{
				currentCar.SupplementPrice = (int?)Supplement.Supplement_MidTermPrice5;
			}
			_context.SaveChanges();
			return RedirectToAction("ShoppingCar");
		}
		// 減少購物車商品數量
		public ActionResult ItemMinus(int SupplementId)
		{
			string UserName = User.Identity.Name;
			var currentCar = _context.OrderDetails.FirstOrDefault(m => m.SupplementId == SupplementId && m.IsApproved == "否" && m.UserName == UserName);
			var Supplement = _context.HealthSupplements.FirstOrDefault(m => m.SupplementId == SupplementId);
			currentCar.Quantity--;
			if (currentCar.Quantity < 5)
			{
				currentCar.SupplementPrice = (int?)Supplement.Supplement_Price;
			}
			else if (currentCar.Quantity < 10)
			{
				currentCar.SupplementPrice = (int?)Supplement.Supplement_MidTermPrice5;
			}
			_context.SaveChanges();
			return RedirectToAction("ShoppingCar");
		}

		// 刪除購物車
		public ActionResult DeleteCar(int OrderDetailId)
		{
			var Order1 = _context.OrderDetails.FirstOrDefault(m => m.OrderDetailId == OrderDetailId);
			Console.WriteLine("Supplement1的結果:" + Order1);
			if (Order1 != null)
			{
				_context.OrderDetails.Remove(Order1);
				_context.SaveChanges();
			}
			return RedirectToAction("ShoppingCar");
		}

		// 送出購物車訂單
		[HttpPost]
		public async Task<ActionResult> ShoppingCarAsync(string Receiver, string Email, string City, string District, string Address, int DetailTotal)
		{
			string? IsLogIn = User.FindFirst(ClaimTypes.Name)?.Value;

			var member = await _context.Members.FirstOrDefaultAsync(m => m.UserName == IsLogIn);

			if (member != null)
			{
				string UserName = User.Identity.Name;
				string guid = Guid.NewGuid().ToString("N").Substring(0, 6); //產生隨機訂單編號

				//加入訂單至 Order 資料表
				var order = new Order();
				order.OrderGuid = guid;
				order.UserName = UserName;
				order.Receiver = Receiver;
				order.Email = Email;
				order.City = City;
				order.District = District;
				order.Address = Address;
				order.Date = DateTime.Now;
				order.Status = "準備中";
				order.TotalPrice = DetailTotal;
				_context.Orders.Add(order);

				//訂單加入後，需一併更新訂單明細內容
				
				var cachedData = _cache.GetString("MyObject");
				var myObject = JsonSerializer.Deserialize<List<OrderDetail>>(cachedData);
				foreach (var item in myObject)
				{
					item.OrderGuid = guid;
					item.IsApproved = "是";
					_context.OrderDetails.Add(item);

				}
				_context.SaveChanges();
				return RedirectToAction("OrderList");
			}
			else
			{
				return RedirectToAction("LogIn", "Members");
			}

		}

		// 會員訂單列表
		public ActionResult OrderList()
		{
			string UserName = User.Identity.Name;
			var orders = _context.Orders.Where(m => m.UserName == UserName).OrderByDescending(m => m.Date).ToList();
			return View(orders);
		}

		// 訂單明細
		public ActionResult OrderDetail(string OrderGuid)
		{
			string OrderGuid2 = OrderGuid;
			List<OrderDetail> orderDetails = _context.OrderDetails.Where((OrderDetail m) => m.OrderGuid == OrderGuid2).ToList();
			ViewBag.OrderGuid = OrderGuid2;
			return View(orderDetails);
		}
	}
}
