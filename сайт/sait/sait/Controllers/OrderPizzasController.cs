﻿using System;
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
    public class OrderPizzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderPizzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderPizzas
        public async Task<IActionResult> Index()
        {
            try
            {
                var orderpizza = _context.OrderPizzas.ToListAsync();

                return View(await _context.OrderPizzas.ToListAsync());
            }
            catch
            {
                return View(null);
            }
        }

        // GET: OrderPizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var orderPizzas = await _context.OrderPizzas
                    .FirstOrDefaultAsync(m => m.id == id);
                if (orderPizzas == null)
                {
                    return NotFound();
                }

                return View(orderPizzas);
            }
            catch
            {
                return View(null);
            }
        }

        // GET: OrderPizzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderPizzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,idPizza,idOrder,count")] OrderPizzas orderPizzas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(orderPizzas);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(orderPizzas);
            }
            catch
            {
                return View(null);
            }
        }

        // GET: OrderPizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var orderPizzas = await _context.OrderPizzas.FindAsync(id);
                if (orderPizzas == null)
                {
                    return NotFound();
                }
                return View(orderPizzas);
            }
            catch
            {
                return View(null);
            }
        }

        // POST: OrderPizzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,idPizza,idOrder,count")] OrderPizzas orderPizzas)
        {
            try
            {
                if (id != orderPizzas.id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(orderPizzas);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!OrderPizzasExists(orderPizzas.id))
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
                return View(orderPizzas);
            }
            catch
            {
                return View(null);
            }
        }

        // GET: OrderPizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var orderPizzas = await _context.OrderPizzas
                    .FirstOrDefaultAsync(m => m.id == id);
                if (orderPizzas == null)
                {
                    return NotFound();
                }

                return View(orderPizzas);
            }
            catch
            {
                return View(null);
            }
        }

        // POST: OrderPizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var orderPizzas = await _context.OrderPizzas.FindAsync(id);
                if (orderPizzas != null)
                {
                    _context.OrderPizzas.Remove(orderPizzas);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(null);
            }
        }

        private bool OrderPizzasExists(int id)
        {

            return _context.OrderPizzas.Any(e => e.id == id);
        }
    }
}
