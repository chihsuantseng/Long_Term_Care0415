using Long_Term_Care.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Long_Term_Care.Controllers
{
	public class CareRecordsController : Controller
	{
		private readonly LongTermCareContext _context;

		public CareRecordsController(LongTermCareContext context)
		{
			_context = context;
		}

		// GET: CareRecords
		public async Task<IActionResult> Index()
		{
			//如果是員工登入則帶員工的資料
			//如果是會員登入則帶會員的資料
			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			var login = _context.Members.FirstOrDefault(o => o.UserName == xx);
			var elogin = _context.Employees.FirstOrDefault(o => o.UserName == xx);

			IQueryable<CareRecord> longTermCareContext;
			if (login != null)
			{
				longTermCareContext = _context.CareRecords.Include(c => c.Case).Include(c => c.Employee).Include(c => c.Member).Include(c => c.Reserve).Where(c => c.MemberId == login.MemberId);
			}
			else
			{
				longTermCareContext = _context.CareRecords.Include(c => c.Case).Include(c => c.Employee).Include(c => c.Member).Include(c => c.Reserve).Where(c => c.EmployeeId == elogin.EmployeeId);
			}

			//var longTermCareContext = _context.CareRecords.Include(c => c.Case).Include(c => c.Employee).Include(c => c.Member).Include(c => c.Reserve);
			return View(await longTermCareContext.ToListAsync());
		}

		// GET: CareRecords/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var careRecord = await _context.CareRecords
				.Include(c => c.Case)
				.Include(c => c.Employee)
				.Include(c => c.Member)
				.Include(c => c.Reserve)
				.FirstOrDefaultAsync(m => m.RecordId == id);
			if (careRecord == null)
			{
				return NotFound();
			}

			return View(careRecord);
		}

		// GET: CareRecords/Create
		public IActionResult Create(int reserveId, string caseName, int caseId, int employeeId,
			string employeeName, DateOnly recordDate, TimeOnly recordTime, int memberId)
		{

			var addcare = new CareRecord
			{
				ReserveId = reserveId,
				CaseName = caseName,
				CaseId = caseId,
				EmployeeId = employeeId,
				EmployeeName = employeeName,
				RecordDate = recordDate,
				RecordTime = recordTime,
				MemberId = memberId

			};
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId");
			ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
			ViewData["ReserveId"] = new SelectList(_context.AppointmentForms, "ReserveId", "ReserveId");
			return View(addcare);
		}

		// POST: CareRecords/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("RecordId,ReserveId,MemberId,CaseId,CaseName,EmployeeId,EmployeeName,RecordDate,RecordTime,Temperature,Heartbeat,Breathe,BefGlucose,AftGlucose,Sbp,Dbp,Memo")] CareRecord careRecord)
		{
			if (ModelState.IsValid)
			{
				_context.Add(careRecord);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", careRecord.CaseId);
			ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", careRecord.EmployeeId);
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", careRecord.MemberId);
			ViewData["ReserveId"] = new SelectList(_context.AppointmentForms, "ReserveId", "ReserveId", careRecord.ReserveId);
			return View(careRecord);
		}

		// GET: CareRecords/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var careRecord = await _context.CareRecords.FindAsync(id);
			if (careRecord == null)
			{
				return NotFound();
			}
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", careRecord.CaseId);
			ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", careRecord.EmployeeId);
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", careRecord.MemberId);
			ViewData["ReserveId"] = new SelectList(_context.AppointmentForms, "ReserveId", "ReserveId", careRecord.ReserveId);
			return View(careRecord);
		}

		// POST: CareRecords/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("RecordId,ReserveId,MemberId,CaseId,CaseName,EmployeeId,EmployeeName,RecordDate,RecordTime,Temperature,Heartbeat,Breathe,BefGlucose,AftGlucose,Sbp,Dbp,Memo")] CareRecord careRecord)
		{
			if (id != careRecord.RecordId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(careRecord);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CareRecordExists(careRecord.RecordId))
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
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", careRecord.CaseId);
			ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", careRecord.EmployeeId);
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", careRecord.MemberId);
			ViewData["ReserveId"] = new SelectList(_context.AppointmentForms, "ReserveId", "ReserveId", careRecord.ReserveId);
			return View(careRecord);
		}

		// GET: CareRecords/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var careRecord = await _context.CareRecords
				.Include(c => c.Case)
				.Include(c => c.Employee)
				.Include(c => c.Member)
				.Include(c => c.Reserve)
				.FirstOrDefaultAsync(m => m.RecordId == id);
			if (careRecord == null)
			{
				return NotFound();
			}

			return View(careRecord);
		}

		// POST: CareRecords/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var careRecord = await _context.CareRecords.FindAsync(id);
			if (careRecord != null)
			{
				_context.CareRecords.Remove(careRecord);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CareRecordExists(int id)
		{
			return _context.CareRecords.Any(e => e.RecordId == id);
		}
	}
}
