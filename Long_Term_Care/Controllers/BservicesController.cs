using Long_Term_Care.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Long_Term_Care.Controllers
{
	public class BservicesController : Controller
	{
		private readonly LongTermCareContext _context;

		public BservicesController(LongTermCareContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> SelectPage()
		{
			//取得登入者的UserName
			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			var login = _context.Members.FirstOrDefault(o => o.UserName == xx);
			ViewData["login"] = login.MemberId;
			//var logincase = _context.Cases.FirstOrDefault(o => o.MemberId == login.MemberId);//取得登入者的Case資料(物件)
			//尋找登入者的Case資料
			var logincases = from p in _context.Cases
							 where p.MemberId == login.MemberId
							 select new Case
							 {
								 CaseId = p.CaseId,
								 CaseName = p.CaseName
							 };

			return View(logincases.ToList());
		}
		// GET: Bservices
		public async Task<IActionResult> Index()
		{
			var longTermCareContext = _context.Bservices.Include(b => b.Case).Include(b => b.Member);
			return View(await longTermCareContext.ToListAsync());
		}

		// GET: Bservices/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var bservice = await _context.Bservices
				 .Include(b => b.Case)
				 .Include(b => b.Member)
				 .FirstOrDefaultAsync(m => m.BplanId == id);
			if (bservice == null)
			{
				return NotFound();
			}

			return View(bservice);
		}

		// GET: Bservices/Create
		public IActionResult Create()
		{
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId");
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
			return View();
		}

		// POST: Bservices/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("BplanId,MemberId,Bdate,Addtotal,Balnum,Ownexp,CmsRanged,Cms,CaseId")] Bservice bservice)
		{
			if (ModelState.IsValid)
			{
				_context.Add(bservice);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", bservice.CaseId);
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", bservice.MemberId);
			return View(bservice);
		}

		// GET: Bservices/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{

			//取得登入者的UserName
			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			var login = _context.Members.FirstOrDefault(o => o.UserName == xx);

			if (id == null)
			{

				return NotFound();

			}
			//讓參數id等於CaseId
			var bservice = await _context.Bservices.FirstOrDefaultAsync(b => b.CaseId == id);
			if (bservice == null)
			{
				//如果Bservice表不存在此個案id，則新增一筆
				var CaseId = id.Value;
				var caseExists = await _context.Bservices.AnyAsync(c => c.CaseId == CaseId);
				if (!caseExists)
				{
					var todaydate = DateTime.Today.Date;
					var newBservice = new Bservice { MemberId = login.MemberId, CaseId = CaseId, Bdate = new(todaydate.Year, todaydate.Month, todaydate.Day), Addtotal = 0, Balnum = 0, Ownexp = 0, CmsRanged = "0", Cms = 0 };

					// Add the new Bservice to the context
					_context.Bservices.Add(newBservice);
					await _context.SaveChangesAsync();
					bservice = newBservice;
				}
				//else
				//{

				//    //return NotFound();
				//}
			}
			//判斷是否為新的月份，若是則歸零
			var today = DateTime.Today.Date;
			var todaymon = DateTime.Today.Month;
			int newmonth = Convert.ToInt32(todaymon);
			var oldmonth = bservice.Bdate.Value.Month;
			int omonth = Convert.ToInt32(oldmonth);
			if (newmonth != omonth)
			{
				bservice.Bdate = new(today.Year, today.Month, today.Day);
				bservice.Addtotal = 0;
				bservice.Balnum = 0;
				bservice.Ownexp = 0;
				bservice.Cms = 0;
				bservice.CmsRanged = "0";
				await _context.SaveChangesAsync();
			}


			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", bservice.CaseId);
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", bservice.MemberId);
			return View(bservice);



		}

		// POST: Bservices/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("BplanId,MemberId,Bdate,Addtotal,Balnum,Ownexp,CmsRanged,Cms,CaseId")] Bservice bservice)
		{

			if (id != bservice.CaseId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{

					_context.Update(bservice);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BserviceExists(bservice.BplanId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction("Create", "AppointmentForms");
			}
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", bservice.CaseId);
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", bservice.MemberId);
			return View(bservice);
		}

		// GET: Bservices/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var bservice = await _context.Bservices
				.Include(b => b.Case)
				.Include(b => b.Member)
				.FirstOrDefaultAsync(m => m.BplanId == id);
			if (bservice == null)
			{
				return NotFound();
			}

			return View(bservice);
		}

		// POST: Bservices/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var bservice = await _context.Bservices.FindAsync(id);
			if (bservice != null)
			{
				_context.Bservices.Remove(bservice);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool BserviceExists(int id)
		{
			return _context.Bservices.Any(e => e.BplanId == id);
		}
	}
}
