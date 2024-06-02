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

            var orders = await _context.Orders.ToListAsync();
            
            return View(orders);
        }
        public async Task<IActionResult> OrderDetails(int id)
        {
            var pizzas = _context.Pizzas.Where(p => _context.OrderPizzas.Any(o => o.idOrder == id && p.id == o.idPizza)).ToList();
           // var PIZZAS = _context.Pizzas.Where(x => x.id == orders.idPizza);

            return View(pizzas);
        }
    }
}
