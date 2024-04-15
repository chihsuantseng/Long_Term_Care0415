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
    public class GservicesController : Controller
    {
        private readonly LongTermCareContext _context;

        public GservicesController(LongTermCareContext context)
        {
            _context = context;
        }

        // GET: Gservices
        public async Task<IActionResult> Index()
        {
            var longTermCareContext = _context.Gservices.Include(g => g.Case).Include(g => g.Member);
            return View(await longTermCareContext.ToListAsync());
        }

        // GET: Gservices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gservice = await _context.Gservices
                .Include(g => g.Case)
                .Include(g => g.Member)
                .FirstOrDefaultAsync(m => m.GplanId == id);
            if (gservice == null)
            {
                return NotFound();
            }

            return View(gservice);
        }

        // GET: Gservices/Create
        public IActionResult Create()
        {
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId");
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return View();
        }

        // POST: Gservices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GplanId,MemberId,CaseId,Gdate,Gaddtotal,Gbalnum,Gownexp,GcmsRanged,Gcms")] Gservice gservice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gservice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", gservice.CaseId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", gservice.MemberId);
            return View(gservice);
        }

        // GET: Gservices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //取得登入者的UserName
            string xx = User.FindFirst(ClaimTypes.Name)?.Value;
            var login = _context.Members.FirstOrDefault(o => o.UserName == xx);
            if (id == null)
            {
                return NotFound();
            }
			var gservice = await _context.Gservices.FirstOrDefaultAsync(b => b.CaseId == id);
            if (gservice == null)
			{
                //如果Gservice表不存在此個案id，則新增一筆
                var CaseId = id.Value;
                var caseExists = await _context.Gservices.AnyAsync(c => c.CaseId == CaseId);
                if (!caseExists)
                {
                    var todaydate = DateTime.Today.Date;
                    var newGservice = new Gservice { MemberId = login.MemberId, CaseId = CaseId, Gdate = new(todaydate.Year, todaydate.Month, todaydate.Day), Gaddtotal = 0, Gbalnum = 0, Gownexp = 0, GcmsRanged = "0", Gcms = 0 };

                    // Add the new Bservice to the context
                    _context.Gservices.Add(newGservice);
                    await _context.SaveChangesAsync();
                    gservice = newGservice;
                }
            }
			//判斷是否為新的年份，若是則歸零
			var today = DateTime.Today.Date;
			var todayyear = DateTime.Today.Year;
			int newyear = Convert.ToInt32(todayyear);
			var oldyear = gservice.Gdate.Value.Year;
			int oyear = Convert.ToInt32(oldyear);
			if (newyear != oyear)
			{

				gservice.Gdate = new(today.Year, today.Month, today.Day);
				gservice.Gaddtotal = 0;
				gservice.Gbalnum = 0;
				gservice.Gownexp = 0;
				gservice.Gcms = 0;
				gservice.GcmsRanged = "0";
				await _context.SaveChangesAsync();
			}


            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", gservice.CaseId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", gservice.MemberId);
            return View(gservice);
        }

        // POST: Gservices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GplanId,MemberId,CaseId,Gdate,Gaddtotal,Gbalnum,Gownexp,GcmsRanged,Gcms")] Gservice gservice)
        {
            if (id != gservice.CaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gservice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GserviceExists(gservice.GplanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CreateG", "AppointmentForms");
            }
            ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", gservice.CaseId);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", gservice.MemberId);
            return View(gservice);
        }

        // GET: Gservices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gservice = await _context.Gservices
                .Include(g => g.Case)
                .Include(g => g.Member)
                .FirstOrDefaultAsync(m => m.GplanId == id);
            if (gservice == null)
            {
                return NotFound();
            }

            return View(gservice);
        }

        // POST: Gservices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gservice = await _context.Gservices.FindAsync(id);
            if (gservice != null)
            {
                _context.Gservices.Remove(gservice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GserviceExists(int id)
        {
            return _context.Gservices.Any(e => e.GplanId == id);
        }
    }
}
