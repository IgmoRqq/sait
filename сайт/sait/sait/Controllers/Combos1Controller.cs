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
    public class Combos1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Combos1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Combos1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Combos.ToListAsync());
        }

        // GET: Combos1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combos = await _context.Combos
                .FirstOrDefaultAsync(m => m.id == id);
            if (combos == null)
            {
                return NotFound();
            }

            return View(combos);
        }

        // GET: Combos1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Combos1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,desription,createDate,price,idCategory")] Combos combos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(combos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(combos);
        }

        // GET: Combos1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combos = await _context.Combos.FindAsync(id);
            if (combos == null)
            {
                return NotFound();
            }
            return View(combos);
        }

        // POST: Combos1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,desription,createDate,price,idCategory")] Combos combos)
        {
            if (id != combos.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(combos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CombosExists(combos.id))
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
            return View(combos);
        }

        // GET: Combos1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var combos = await _context.Combos
                .FirstOrDefaultAsync(m => m.id == id);
            if (combos == null)
            {
                return NotFound();
            }

            return View(combos);
        }

        // POST: Combos1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var combos = await _context.Combos.FindAsync(id);
            if (combos != null)
            {
                _context.Combos.Remove(combos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CombosExists(int id)
        {
            return _context.Combos.Any(e => e.id == id);
        }
    }
}
