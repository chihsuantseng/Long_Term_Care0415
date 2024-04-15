using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Long_Term_Care.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder.Extensions;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Caching.Distributed;
//using Google.Apis.Auth;

namespace Long_Term_Care.Controllers
{
	public class MembersController : Controller
	{
		private readonly LongTermCareContext _context;
        private readonly IDistributedCache _cache;
        public MembersController(LongTermCareContext context, IDistributedCache distributedCache)
        {
			_context = context;
            _cache = distributedCache;
        }

		// GET: Members
		public async Task<IActionResult> Index()
		{
			var longTermCareContext = _context.Members.Include(m => m.Case);

			return View(await longTermCareContext.ToListAsync());
		}



		public IActionResult LogIn()
		{
			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			var member = _context.Members.FirstOrDefault(m => m.UserName == xx);
			var employee = _context.Employees.FirstOrDefault(o => o.UserName == xx);

			if (string.IsNullOrEmpty(xx))
			{
				return View();
			}
			if (member != null)
			{
				return RedirectToAction("Edit", new { id = member.MemberId });
			}
			if (employee != null)
			{
				return RedirectToAction("EditEmployee", "Employees", new { id = employee.EmployeeId });
			}
			return NotFound();
		}
		[HttpPost]

		public async Task<IActionResult> LogIn(string account, string password)
		{
			if (account == null)
			{

				return View("LogIn");
			}
            string key = "newpassword";
			var value = await _cache.GetStringAsync(key);

            var access = await _context.Members
				.FirstOrDefaultAsync(m => m.UserName == account && m.Password == password);
            var access2 = await _context.Members
                .FirstOrDefaultAsync(m => m.UserName == account);
            var accessemployee = await _context.Employees.FirstOrDefaultAsync(m => m.UserName == account && m.Password == password);
            if (access2 != null && value != null)
            {
                access2.Password = value;
                _context.Update(access2);
                await _context.SaveChangesAsync();
                var accessx = access2.MemberId;
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, access2.UserName),
            new Claim("FullName", access2.MemberName),
             new Claim("MemberId", access2.MemberId.ToString()),
            new Claim(ClaimTypes.Role, "Administrator"),
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Edit", new { id = accessx });


            }
            if (access != null)
			{
				var x = access!.MemberId;
				var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, access.UserName),
			new Claim("FullName", access.MemberName),
			 new Claim("MemberId", access.MemberId.ToString()),
			new Claim(ClaimTypes.Role, "Administrator"),
		};

				var claimsIdentity = new ClaimsIdentity(
					claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var authProperties = new AuthenticationProperties
				{
					//AllowRefresh = <bool>,
					// Refreshing the authentication session should be allowed.

					//ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
					// The time at which the authentication ticket expires. A 
					// value set here overrides the ExpireTimeSpan option of 
					// CookieAuthenticationOptions set with AddCookie.

					//IsPersistent = true,
					// Whether the authentication session is persisted across 
					// multiple requests. When used with cookies, controls
					// whether the cookie's lifetime is absolute (matching the
					// lifetime of the authentication ticket) or session-based.

					//IssuedUtc = <DateTimeOffset>,
					// The time at which the authentication ticket was issued.

					//RedirectUri = <string>
					// The full path or absolute URI to be used as an http 
					// redirect response value.
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProperties);
				return RedirectToAction("Edit", new { id = x });

			}
			if (accessemployee != null)
			{
				var x1 = accessemployee!.EmployeeId;

				var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, accessemployee.UserName),
			new Claim("FullName", accessemployee.EmployeeName),
			new Claim(ClaimTypes.Role, "Administrator"),
		};

				var claimsIdentity = new ClaimsIdentity(
					claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var authProperties = new AuthenticationProperties
				{
					//AllowRefresh = <bool>,
					// Refreshing the authentication session should be allowed.

					//ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
					// The time at which the authentication ticket expires. A 
					// value set here overrides the ExpireTimeSpan option of 
					// CookieAuthenticationOptions set with AddCookie.

					//IsPersistent = true,
					// Whether the authentication session is persisted across 
					// multiple requests. When used with cookies, controls
					// whether the cookie's lifetime is absolute (matching the
					// lifetime of the authentication ticket) or session-based.

					//IssuedUtc = <DateTimeOffset>,
					// The time at which the authentication ticket was issued.

					//RedirectUri = <string>
					// The full path or absolute URI to be used as an http 
					// redirect response value.
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProperties);
				return RedirectToAction("EditEmployee", "Employees", new { id = x1 });

			}
			ViewBag.Error = "帳密驗證失敗";
			return View("LogIn");




		}

		public async Task<ActionResult> Logout()
		{
			string xx = User.FindFirst("FullName")?.Value;
			if (!string.IsNullOrEmpty(xx))
			{
				ModelState.AddModelError(string.Empty, xx);
			}

			// Clear the existing external cookie
			await HttpContext.SignOutAsync(
				CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("LogIn");
		}


		[Authorize]
		//GET: Members/Edit/5
		public async Task<IActionResult> Edit(int id)
		{


			if (id == null)
			{

				return RedirectToPage("/Members/LogIn");

			}


			string xx = User.FindFirst(ClaimTypes.Name)?.Value;

			//var member = await _context.Members.FindAsync(id);
			var member = await _context.Members.FirstOrDefaultAsync(m => m.UserName == xx);

			member.Cases = await _context.Cases.Where(x => x.MemberId == member.MemberId).ToListAsync();


			if (member == null)
			{
				return RedirectToPage("/Members/LogIn");
			}


			if (!string.IsNullOrEmpty(xx))
			{


				ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", member.CaseId);
				return View(member);

			}


			return RedirectToPage("/Members/LogIn");
		}


		// POST: Members/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("MemberId,UserName,Password,MemberAvatar,MemberName,Gender,Email,HomePhone,MobilePhone,City,District,Address,CaseId")] Member member)
		{

			if (id != member.MemberId)
			{
				return NotFound();
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
							member.MemberAvatar = dataStream.ToArray();
						}
					}
					_context.Update(member);


					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MemberExists(member.MemberId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Edit), new { id = id });
			}

			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", member.CaseId);
			return View(member);
		}


		public async Task<IActionResult> CreateCase()
		{
			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			//var member = await _context.Members.FindAsync(id);


			var member = await _context.Members.FirstOrDefaultAsync(m => m.UserName == xx);
			ViewBag.FamName = member.MemberName;
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", member.MemberId);
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateCase([Bind("CaseId,CaseName,Gender,Birthdate,Age,IdentificationNumber,IdentityType,CasePhone,FamilyPhone,FamilyName,City,District,Address,Relation,MemberId")] Case Cases)
		{
			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			//var member = await _context.Members.FindAsync(id);
			var member = await _context.Members.FirstOrDefaultAsync(m => m.UserName == xx);

			if (ModelState.IsValid)
			{

				_context.Add(Cases);
				var xxx = _context.Cases.OrderByDescending(c => c.CaseId).Select(O => O.CaseId).FirstOrDefault();
				member.CaseId = xxx;
				_context.Members.Update(member);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Edit), new { id = member.MemberId });
			}
			ViewData["CaseId"] = new SelectList(_context.Members, "MemberId", "MemberId", Cases.MemberId);
			return View(Cases);
		}
		[Authorize]
		//GET: Members/EditCase/5
		public async Task<IActionResult> EditCase(int id)
		{
			if (id == null)
			{

				return RedirectToPage("/Members/LogIn");

			}

			//string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			//var member = await _context.Members.FindAsync(id);
			var Cases = await _context.Cases.FirstOrDefaultAsync(m => m.CaseId == id);
			if (Cases == null)
			{
				return RedirectToPage("/Members/LogIn");
			}
			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", Cases.MemberId);
			return View(Cases);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditCase(int id, [Bind("CaseId,CaseAvatar,CaseName,Gender,Birthdate,Age,IdentificationNumber,IdentityType,CasePhone,FamilyPhone,FamilyName,City,District,Address,Relation,MemberId")] Case Cases)
		{
			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			//var member = await _context.Members.FindAsync(id);
			var member = await _context.Members.FirstOrDefaultAsync(m => m.UserName == xx);
			if (id != Cases.CaseId)
			{
				return NotFound();
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
							Cases.CaseAvatar = dataStream.ToArray();
						}
					}

					_context.Update(Cases);


					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CaseExists(Cases.CaseId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Edit), new { id = member.MemberId });
			}

			ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", Cases.MemberId);
			return View(Cases);
		}




		// POST: Members/Delete/5

		public async Task<IActionResult> DeleteCase(int id)
		{
			string xx = User.FindFirst(ClaimTypes.Name)?.Value;
			var members = await _context.Members.FirstOrDefaultAsync(o => o.UserName == xx);
			var Cases1 = await _context.Cases.FindAsync(id);
			if (Cases1 != null)
			{
				_context.Cases.Remove(Cases1);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Edit), new { id = members.MemberId });
		}

		private bool MemberExists(int id)
		{
			return _context.Members.Any(e => e.MemberId == id);
		}
		private bool CaseExists(int id)
		{
			return _context.Cases.Any(e => e.CaseId == id);
		}

		public IActionResult SignUp()
		{
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignUp([Bind("MemberId,UserName,Password,MemberAvatar,MemberName,Gender,Email,HomePhone,MobilePhone,City,District,Address,CaseId")] Member member)
		{
			if (ModelState.IsValid)
			{
				_context.Add(member);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(LogIn));
			}
			ViewData["CaseId"] = new SelectList(_context.Cases, "CaseId", "CaseId", member.CaseId);
			return View(member);
		}

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var member = await _context.Members.FirstOrDefaultAsync(m => m.UserName == model.Account && m.Email == model.Email);
				if (member != null)
				{

					string newPassword = GenerateRandomPassword();
					//member.Password = newPassword;
					//_context.Update(member);
					//await _context.SaveChangesAsync();
					

					await SendNewPasswordByEmail(member.Email, newPassword);
                    string key = "newpassword";
                    string value = newPassword;
                    TimeSpan expiration = TimeSpan.FromSeconds(60);
                    await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = expiration
                    });


                    if (SendNewPasswordByEmail != null)
					{
						ViewBag.PasswordResetSuccess = true;
						return View();
					}

					return RedirectToAction("LogIn");

				}
				else
				{
					ViewBag.Message = "提供的帳號和電子信箱不匹配，請重新輸入";
				}
			}
			return View(model);
		}

		//[HttpPost]
		//public IActionResult CloseModal()
		//{
		//    return Json(new { url = Url.Action("LogIn", "Members") });
		//}


		private async Task SendNewPasswordByEmail(string userEmail, string newPassword)
		{
			try
			{

				string senderEmail = "joyce07128989@yahoo.com.tw";

				string senderPassword = "accjuwugrbcwmurv";

				string subject = "馨安居家長照-會員密碼重置";

				string body = $"<p style='font-size:20px'>您的新密碼為:  {newPassword}，請在60秒內重新登入,登入後盡速修改密碼!</p>";

				string signature = "<p style='color:red; font-size: 20px; font-weight: bold'>此信件由系統發出，請勿直接回覆！</p>";

				// SMTP
				using (SmtpClient client = new SmtpClient("smtp.mail.yahoo.com"))
				{
					client.Port = 587;
					client.UseDefaultCredentials = false;
					client.Credentials = new NetworkCredential(senderEmail, senderPassword);
					client.EnableSsl = true;


					MailMessage mailMessage = new MailMessage();
					mailMessage.From = new MailAddress(senderEmail);
					mailMessage.To.Add(userEmail);
					mailMessage.Subject = subject;
					mailMessage.IsBodyHtml = true;
					mailMessage.Body = $"{body}<br/><br/><br/>{signature}";


					await client.SendMailAsync(mailMessage);
				}
			}
			catch (Exception ex)
			{

			}
		}

		private string GenerateRandomPassword()
		{
			string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			Random random = new Random();

			return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}

}
