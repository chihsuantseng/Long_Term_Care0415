using Long_Term_Care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Long_Term_Care.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly LongTermCareContext _context;

		public EmployeesController(LongTermCareContext context)
		{
			_context = context;
		}

		[Authorize]
		// GET: Employees/Edit/5
		public async Task<IActionResult> EditEmployee(int id)
		{
			if (id == null)
			{
				return RedirectToPage("/Members/LogIn");
			}

			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			//var member = await _context.Members.FindAsync(id);


			var employee = await _context.Employees.FirstOrDefaultAsync(m => m.UserName == xx);
			if (!string.IsNullOrEmpty(xx))
			{
				return View(employee);

			}


			return RedirectToPage("/Members/LogIn");
		}

		// POST: Employees/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditEmployee(int id, [Bind("EmployeeId,UserName,Password,EmployeeAvatar,EmployeeName,Gender,Birthdate,Age,IdentificationNumber,Email,HomePhone,MobilePhone,City,District,Address,HireDate,EmploymentStatus,Supervisor,Title")] Employee employee)
		{
			if (id != employee.EmployeeId)
			{
				return RedirectToPage("/Members/LogIn");
			}

			if (ModelState.IsValid)
			{
				try
				{
					if (Request.Form.Files.Count > 0)
					{
						IFormFile file = Request.Form.Files.FirstOrDefault();
						using (var dataStream = new MemoryStream())
						{
							await file.CopyToAsync(dataStream);
							employee.EmployeeAvatar = dataStream.ToArray();
						}
					}
					_context.Update(employee);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EmployeeExists(employee.EmployeeId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(EditEmployee), new { id = id });
			}
			return View(employee);
		}
		private bool EmployeeExists(int id)
		{
			return _context.Employees.Any(e => e.EmployeeId == id);
		}

	}
}
