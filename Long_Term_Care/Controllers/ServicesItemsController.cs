using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Long_Term_Care.Models;

namespace Long_Term_Care.Controllers
{
    public class ServicesItemsController : Controller
    {
        private readonly LongTermCareContext _context;

        public ServicesItemsController(LongTermCareContext context)
        {
            _context = context;
        }

        // GET: ServicesItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServicesItems.ToListAsync());
        }

        // GET: ServicesItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesItem = await _context.ServicesItems
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (servicesItem == null)
            {
                return NotFound();
            }

            return View(servicesItem);
        }

        // GET: ServicesItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServicesItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,ServiceName,ServicePrice")] ServicesItem servicesItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicesItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicesItem);
        }

        // GET: ServicesItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesItem = await _context.ServicesItems.FindAsync(id);
            if (servicesItem == null)
            {
                return NotFound();
            }
            return View(servicesItem);
        }

        // POST: ServicesItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ServiceId,ServiceName,ServicePrice")] ServicesItem servicesItem)
        {
            if (id != servicesItem.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicesItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesItemExists(servicesItem.ServiceId))
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
            return View(servicesItem);
        }

        // GET: ServicesItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesItem = await _context.ServicesItems
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (servicesItem == null)
            {
                return NotFound();
            }

            return View(servicesItem);
        }

        // POST: ServicesItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var servicesItem = await _context.ServicesItems.FindAsync(id);
            if (servicesItem != null)
            {
                _context.ServicesItems.Remove(servicesItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesItemExists(string id)
        {
            return _context.ServicesItems.Any(e => e.ServiceId == id);
        }
    }
}
