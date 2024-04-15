using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Long_Term_Care.Models;
using System.Security.Claims;

namespace Long_Term_Care.Controllers
{
    public class AppointmentFormsController : Controller
    {
        private readonly LongTermCareContext _context;

        public AppointmentFormsController(LongTermCareContext context)
        {
            _context = context;
        }

		public async Task<IActionResult> Schedule()
		{
			string xx = User.FindFirst(ClaimTypes.Name)?.Value!;
			var employee = await _context.Employees.FirstOrDefaultAsync(m => m.UserName == xx);
            if (string.IsNullOrEmpty(xx))
            {
                return View(employee);

            }

            var result = _context.AppointmentForms.Where(o => o.EmployeeName == employee.EmployeeName).ToList();

            ViewBag.name = employee.EmployeeName;
            return View(result);
		}

		// GET: AppointmentForms
		public async Task<IActionResult> Index()
        {
            
            string xx = User.FindFirst(ClaimTypes.Name)?.Value;
            var elogin = _context.Employees.FirstOrDefault(o => o.UserName == xx);
            var longTermCareContext = _context.AppointmentForms.Include(a => a.Case).Include(a => a.Employee).Include(a => a.Member).Include(a => a.Service).Where(a => a.EmployeeId==elogin.EmployeeId);
            //var longTermCareContext = _context.AppointmentForms.Include(a => a.Case).Include(a => a.Employee).Include(a => a.Member).Include(a => a.Service);
            return View(await longTermCareContext.ToListAsync());
        }

        // GET: AppointmentForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentForm = await _context.AppointmentForms
                .Include(a => a.Case)
                .Include(a => a.Employee)
                .Include(a => a.Member)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.ReserveId == id);
            if (appointmentForm == null)
            {
                return NotFound();
            }

            return View(appointmentForm);
        }

        int memberId;
        // GET: AppointmentForms/Create
        public IActionResult Create()
        {
            // 獲取Cookie中的MemberId
            var memberIdClaim = HttpContext.User.FindFirst("MemberId");
            memberId = 0; // 預設值

            if (memberIdClaim != null && int.TryParse(memberIdClaim.Value, out memberId))
            {

                // 隨機查詢 EmployeeId 和 EmployeeName
                var (randomEmployeeId, randomEmployeeName) = GetRandomEmployeeIdAndName();

                // 將隨機查詢到的 EmployeeId 和 EmployeeName 設置到 ViewData 中
                ViewData["SelectedEmployeeId"] = randomEmployeeId;
                ViewData["SelectedEmployeeName"] = randomEmployeeName;

            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            ViewData["ServiceId"] = new SelectList(_context.ServicesItems, "ServiceId", "ServiceId");
            
            string url = Url.Action("Edit", "Members", new { id = memberId });
            ViewData["RedirectUrl"] = url;
            return View();
        }

        // GET: AppointmentForms/CreateG
        public IActionResult CreateG()
        {
            // 獲取Cookie中的MemberId
            var memberIdClaim = HttpContext.User.FindFirst("MemberId");
            memberId = 0; // 預設值

            if (memberIdClaim != null && int.TryParse(memberIdClaim.Value, out memberId))
            {
                
                // 隨機查詢 EmployeeId 和 EmployeeName
                var (randomEmployeeId, randomEmployeeName) = GetRandomEmployeeIdAndName();

                // 將隨機查詢到的 EmployeeId 和 EmployeeName 設置到 ViewData 中
                ViewData["SelectedEmployeeId"] = randomEmployeeId;
                ViewData["SelectedEmployeeName"] = randomEmployeeName;

            }

            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            ViewData["ServiceId"] = new SelectList(_context.ServicesItems, "ServiceId", "ServiceId");

            string gurl = Url.Action("Edit", "Members", new { id = memberId });
            ViewData["GRedirectUrl"] = gurl;
            
            return View();
        }

        //先隨機指派照服員到前端
        private (int, string) GetRandomEmployeeIdAndName()
        {
            // 取得 Employee 表中的所有 EmployeeId 和 EmployeeName
            var allEmployees = _context.Employees.Select(e => new { e.EmployeeId, e.EmployeeName }).ToList();

            // 從所有 Employee 中隨機選擇一個
            var random = new Random();
            var randomIndex = random.Next(0, allEmployees.Count);
            var randomEmployee = allEmployees[randomIndex];

            return (randomEmployee.EmployeeId, randomEmployee.EmployeeName);
        }

        //表單傳到後端，先檢查用戶選擇的日期時段，照服員是否都已有班
        private bool AreAllEmployeesAssigned(AppointmentForm appointmentForm)
        {
            var verifyAppointment = _context.AppointmentForms
                .Where(a => a.WorkingDate == appointmentForm.WorkingDate &&
                            a.StartTime == appointmentForm.StartTime &&
                            a.EndTime == appointmentForm.EndTime)
                .ToList();

            var allEmployeeIds = _context.Employees.Select(e => e.EmployeeId).ToList();

            return allEmployeeIds.All(id => verifyAppointment.Any(a => a.EmployeeId == id));
        }

        //隨機指派沒班的照服員
        private void AssignRandomEmployee(AppointmentForm appointmentForm)
        {
            var overlapAppointment = _context.AppointmentForms
            .FirstOrDefault(a => a.EmployeeId == appointmentForm.EmployeeId &&
                                 a.EmployeeName == appointmentForm.EmployeeName &&
                                 a.WorkingDate == appointmentForm.WorkingDate &&
                                 a.StartTime == appointmentForm.StartTime &&
                                 a.EndTime == appointmentForm.EndTime);

            // 如果完全重疊，則隨機選擇一個不重疊的 EmployeeId 和 EmployeeName
            if (overlapAppointment != null)
            {
                //取得表單裡的 EmployeeId
                var currentEmployeeId = appointmentForm.EmployeeId;

                // 取得所有 EmployeeId
                var allEmployeeIds = _context.Employees.Select(e => e.EmployeeId).ToList();

                //從所有 EmployeeId 中排除當前的 EmployeeId
                var availableEmployeeIds = allEmployeeIds.Except(new List<int> { currentEmployeeId }).ToList();

                //隨機指派可用的 EmployeeId 和 EmployeeName
                Random random = new Random();
                var randomEmployeeId = availableEmployeeIds[random.Next(availableEmployeeIds.Count)];
                var randomEmployeeName = _context.Employees
                                .Where(e => e.EmployeeId == randomEmployeeId)
                                .Select(e => e.EmployeeName)
                                .FirstOrDefault();

                //將隨機指派的 EmployeeId 和 EmployeeName 重傳到預約表單
                appointmentForm.EmployeeId = randomEmployeeId;
                appointmentForm.EmployeeName = randomEmployeeName!;

                //遞迴調用，防止隨機指派的照服員再重疊
                AssignRandomEmployee(appointmentForm);
            }
        }


        // POST: AppointmentForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReserveId,MemberId,ReserveTime,CaseId,CaseAvatar,CaseName,EmployeeId,EmployeeName,ServiceId,WorkingDate,StartTime,EndTime")] AppointmentForm appointmentForm)
        {
            // 獲取Cookie中的MemberId
            var memberIdClaim = HttpContext.User.FindFirst("MemberId");
            memberId = 0; // 預設值
            //if (memberIdClaim != null && int.TryParse(memberIdClaim.Value, out memberId))
            //{
            //    // 如果Cookie中存在MemberId，設置到提交的表單中
            //    appointmentForm.MemberId = memberId;

            //    // 根據已登入的MemberId查詢CaseId
            //    var caseId = _context.Cases
            //        .Where(c => c.MemberId == memberId)
            //        .Select(c => c.CaseId)
            //        .FirstOrDefault();
            //    // 將獲取到的CaseId設置到提交的表單中
            //    appointmentForm.CaseId = caseId;

            //    var caseAvatar = _context.Cases
            //        .Where(c => c.MemberId == memberId)
            //        .Select(c => c.CaseAvatar)
            //    .FirstOrDefault();
            //    // 將獲取到的CaseAvatar設置到提交的表單中
            //    appointmentForm.CaseAvatar = caseAvatar;

            //}

            //若是一對多關係
            if (memberIdClaim != null && int.TryParse(memberIdClaim.Value, out memberId))
            {
                // 如果Cookie中存在MemberId，設置到提交的表單中
                appointmentForm.MemberId = memberId;
            }

            // 根據用戶選擇的CaseName查詢相應的CaseId和CaseAvatar
            var selectedCase = _context.Cases
                .FirstOrDefault(c => c.CaseName == appointmentForm.CaseName);

            if (selectedCase != null)
            {
                // 如果找到匹配的案例，設置到提交的表單中
                appointmentForm.CaseId = selectedCase.CaseId;
                appointmentForm.CaseAvatar = selectedCase.CaseAvatar;
            }
            else
            {
                // 如果未找到匹配的案例，您可能需要處理這種情況
                // 例如，設置默認值或返回錯誤視圖
            }

            //如果用戶選擇的日期時段，照服員都已有班，則在前端跳出警告
            if (AreAllEmployeesAssigned(appointmentForm))
            {
                ViewData["IsAllEmployeesAssigned"] = true;
                ViewBag.WorkingDate = appointmentForm.WorkingDate;
                ViewBag.StartTime = appointmentForm.StartTime;
                ViewBag.EndTime = appointmentForm.EndTime;
                return View(appointmentForm);
            }

            //隨機指派沒班的照服員
            AssignRandomEmployee(appointmentForm);

            if (ModelState.IsValid)
            {
                _context.Add(appointmentForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", appointmentForm.CaseId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", appointmentForm.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", appointmentForm.MemberId);
            ViewData["ServiceId"] = new SelectList(_context.ServicesItems, "ServiceId", "ServiceId", appointmentForm.ServiceId);
            return View(appointmentForm);
        }

        // GET: AppointmentForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentForm = await _context.AppointmentForms.FindAsync(id);
            if (appointmentForm == null)
            {
                return NotFound();
            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", appointmentForm.CaseId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", appointmentForm.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", appointmentForm.MemberId);
            ViewData["ServiceId"] = new SelectList(_context.ServicesItems, "ServiceId", "ServiceId", appointmentForm.ServiceId);
            return View(appointmentForm);
        }

        // POST: AppointmentForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReserveId,MemberId,ReserveTime,CaseId,CaseAvatar,CaseName,EmployeeId,EmployeeName,ServiceId,WorkingDate,StartTime,EndTime")] AppointmentForm appointmentForm)
        {
            if (id != appointmentForm.ReserveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentFormExists(appointmentForm.ReserveId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", appointmentForm.CaseId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", appointmentForm.EmployeeId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", appointmentForm.MemberId);
            ViewData["ServiceId"] = new SelectList(_context.ServicesItems, "ServiceId", "ServiceId", appointmentForm.ServiceId);
            return View(appointmentForm);
        }

        // GET: AppointmentForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentForm = await _context.AppointmentForms
                .Include(a => a.Case)
                .Include(a => a.Employee)
                .Include(a => a.Member)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.ReserveId == id);
            if (appointmentForm == null)
            {
                return NotFound();
            }

            return View(appointmentForm);
        }

        // POST: AppointmentForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointmentForm = await _context.AppointmentForms.FindAsync(id);
            if (appointmentForm != null)
            {
                _context.AppointmentForms.Remove(appointmentForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentFormExists(int id)
        {
            return _context.AppointmentForms.Any(e => e.ReserveId == id);
        }
    }
}
