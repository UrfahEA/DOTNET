using dotnet_cookie.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dotnet_cookie.Controllers
{
    public class AccountController : Controller
    {
        public StudentDbContext _context { get; set; }

        public AccountController(StudentDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Student std)
        {
            if (ModelState.IsValid)
            {
                _context.tbl_Students.Add(std);
                _context.SaveChanges();
                TempData["Success"] = "Registration Sucessful";
                return RedirectToAction("Login", "Account");

            }
            return View(std);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var std = _context.tbl_Students.FirstOrDefault
                (x => x.email == email && x.password == password);

            if (std != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, std.email),
                    new Claim("Student ID",std.id.ToString()),
                };

                var identity = new ClaimsIdentity
                    (claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}