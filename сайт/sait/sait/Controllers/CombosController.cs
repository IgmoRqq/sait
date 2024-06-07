using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sait.DataBase;
using sait.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using sait.Extensions;
using sait.ViewModels;

namespace sait.Controllers
{
    public class CombosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CombosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var combos = await _context.Combos.ToListAsync();
                return View(combos);
            }
            catch
            {
                return View(null);
            }
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            try
            {
                var combo = _context.Combos.FirstOrDefault(c => c.id == id);
                if (combo != null)
                {
                    var cart = HttpContext.Session.GetObjectFromJson<List<CartCombo>>("Cart") ?? new List<CartCombo>();
                    var CartCombo = cart.FirstOrDefault(c => c.Combo.id == id);
                    if (CartCombo == null)
                    {
                        cart.Add(new CartCombo { Combo = combo, Quantity = 1 });
                    }
                    else
                    {
                        CartCombo.Quantity++;
                    }
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error1", "Users1");
            }
        }

        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartCombo>>("Cart") ?? new List<CartCombo>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartCombo>>("Cart") ?? new List<CartCombo>();
            var CartCombo = cart.FirstOrDefault(c => c.Combo.id == id);
            if (CartCombo != null)
            {
                cart.Remove(CartCombo);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Cart");
        }

        public IActionResult PlaceOrder()
        {
            try
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<CartCombo>>("Cart") ?? new List<CartCombo>();

                // Создание нового заказа
                var order = new Orders
                {
                    dateOrder = DateTime.Now,
                    status = "prepare", // Устанавливаем статус заказа, например, "Готов"
                    idUser = CurrentUser.user.id // Укажите ID пользователя, который делает заказ
                };
                _context.Orders.Add(order);
                _context.SaveChanges();

                // Добавление комбинированных блюд в заказ
                foreach (var item in cart)
                {
                    _context.OrderCombos.Add(new OrderCombos
                    {
                        idCombo = item.Combo.id,
                        idOrder = order.id,
                        count = item.Quantity
                    });
                }
                _context.SaveChanges();

                // Очистка корзины после оформления заказа
                HttpContext.Session.Remove("Cart");

                return RedirectToAction("Index", "Home");
            }
            catch 
            {
                return RedirectToAction("Error1", "Users1");
            }
        }
    }
}
