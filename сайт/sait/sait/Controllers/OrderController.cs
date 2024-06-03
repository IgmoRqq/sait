using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sait.DataBase;
using sait.Models;

namespace sait.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Orders()
        {
            try
            {

                var orders = await _context.Orders.ToListAsync();

                return View(orders);
            }
            catch
            {
                return View(null);
            }
        }
        public async Task<IActionResult> OrderDetails(int id)
        {
            try
            {

                List<Pizzas> pizzas = new List<Pizzas>();
                foreach (var a in _context.OrderPizzas.ToList()) // Здесь вызываем ToList()
                {
                    if (a.idOrder == id)
                    {
                        foreach (var b in _context.Pizzas.ToList()) // Здесь также вызываем ToList()
                        {
                            if (b.id == a.idPizza)
                            {
                                //pizzas.Add(b);
                                for (int i = 0; i < a.count; i++)
                                {
                                    pizzas.Add(b);
                                }
                            }
                        }
                    }
                }

                return View(pizzas);
            }
            catch
            {
                return View(null);
            }
        }
        public async Task<IActionResult> OrderComboDetails(int id)
        {
            try
            {
                List<Combos> combos = new List<Combos>();
                foreach (var a in _context.OrderCombos.ToList()) // Здесь вызываем ToList()
                {
                    if (a.idOrder == id)
                    {
                        foreach (var b in _context.Combos.ToList()) // Здесь также вызываем ToList()
                        {
                            if (b.id == a.idCombo)
                            {
                                //pizzas.Add(b);
                                for (int i = 0; i < a.count; i++)
                                {
                                    combos.Add(b);
                                }
                            }
                        }
                    }
                }

                return View(combos);
            }
            catch
            {
                return View(null);
            }
        }
    }
}
