using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sait.DataBase;
using sait.Models;

namespace sait.Controllers
{
    public class Pizzas1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Pizzas1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pizzas1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pizzas.ToListAsync());
        }

        // GET: Pizzas1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.id == id);
            if (pizzas == null)
            {
                return NotFound();
            }

            return View(pizzas);
        }

        // GET: Pizzas1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pizzas1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,size,price,description,idCategory,createDate")] Pizzas pizzas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(pizzas);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(pizzas);
            }
            catch
            {
                return RedirectToAction("Error1", "Users1");
            }
        }

        // GET: Pizzas1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _context.Pizzas.FindAsync(id);
            if (pizzas == null)
            {
                return NotFound();
            }
            return View(pizzas);
        }

        // POST: Pizzas1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,size,price,description,idCategory,createDate")] Pizzas pizzas)
        {
            try
            {
                if (id != pizzas.id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(pizzas);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PizzasExists(pizzas.id))
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
                return View(pizzas);
            }
            catch
            {
                return RedirectToAction("Error1", "Users1");
            }
        }

        

        private bool PizzasExists(int id)
        {
            return _context.Pizzas.Any(e => e.id == id);
        }
    }
}
