using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sait.DataBase;
using sait.Models;
using sait.ViewModels;
using System.Security.Claims;

namespace sait.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.email == model.Email && u.password == model.Password);

                if (user != null)
                {
                    // Аутентификация пользователя
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
                // Добавьте дополнительные утверждения, если необходимо
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        // Запомнить пользователя, если это установлено в true
                        IsPersistent = model.RememberMe,
                        // Дополнительные настройки аутентификации, если необходимо
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Перенаправление пользователя на страницу, которая была запрошена перед входом
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(model);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var isUserExists = await _context.Users.FirstOrDefaultAsync(u => u.email == model.Email);
                if (isUserExists != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким email уже существует");
                    return View(model);
                }
                var newUser = new Users
                {
                    email = model.Email,
                    password = model.Password,
                    createDate = DateTime.Now,
                    idRole = 1,
                    adress = model.Address
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // Вызываем метод SignOutAsync для очистки аутентификационных кук
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Перенаправляем пользователя на главную страницу или на другую страницу
            return RedirectToAction("Index", "Home");
        }


        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                // Перенаправление на MainMenu вместо Index
                return RedirectToAction(nameof(HomeController.MainMenu), "Home");
            }
        }


    }
}
