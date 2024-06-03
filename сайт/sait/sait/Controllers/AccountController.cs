
    using BCrypt.Net; // Добавьте это пространство имен
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
                var user = await _context.Users.FirstOrDefaultAsync(u => u.email == model.Email);

                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.email),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    if (user.idRole == 1)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    else if (user.idRole == 2)
                    {
                        return RedirectToAction("Index", "Home1");
                    }
                }


                ModelState.AddModelError("", "Не удалось войти в аккаунт");
            }
            return View(model);
        }

        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var isUserExists = await _context.Users.FirstOrDefaultAsync(u => u.email == model.Email);
                if (isUserExists != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким email уже существует");
                    return View(model);
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var newUser = new Users
                {
                    email = model.Email,
                    password = hashedPassword,
                    createDate = DateTime.Now,
                    idRole = 1,
                    adress = model.Address
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, newUser.email),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToLocal(returnUrl);
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
