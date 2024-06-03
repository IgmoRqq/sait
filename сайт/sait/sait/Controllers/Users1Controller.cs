using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sait.DataBase;
using sait.ViewModels;

namespace sait.Controllers
{
    public class Users1Controller : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public Users1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users1/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Error1()
        {
            return View();
        }

        // POST: Users1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    email = model.Email,
                    password = model.Password,
                    idRole = model.RoleId,
                    adress = model.Address,
                    createDate = model.CreateDate
                };

                // Сохранение пользователя в базе данных
                // Пример:
                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        
    }

        // GET: Users1/Edit/5
    }
}
