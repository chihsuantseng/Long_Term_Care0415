using Long_Term_Care.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Long_Term_Care.Controllers.PhysicalExaminationController;

namespace Long_Term_Care.Controllers
{
    public class ReservationByDateController : Controller
    {
		private readonly LongTermCareContext _context;

		public ReservationByDateController(LongTermCareContext context)
		{
			_context = context;
		}

		int memberId;
		public IActionResult ReservationByDate()
		{
			// 獲取Cookie中的MemberId
			var memberIdClaim = HttpContext.User.FindFirst("MemberId");
			memberId = 0; // 預設值
			if (memberIdClaim != null && int.TryParse(memberIdClaim.Value, out memberId))
			{
				var appointments = _context.AppointmentForms
					.Where(a => a.MemberId == memberId) // 根據會員Id過濾預約資料
					.ToList(); // 將結果轉換為列表

                var jsonAppointments = JsonConvert.SerializeObject(appointments); // 將資料轉換為 JSON 字符串

                ViewData["AppointmentsJson"] = jsonAppointments; // 將 JSON 字符串傳遞給視圖

                return View();


            }


			var appointmentForms = _context.AppointmentForms.ToList();
			return View(appointmentForms);
		}

	}
}
