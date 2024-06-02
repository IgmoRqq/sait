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
    public class PizzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PizzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pizzas = await _context.Pizzas.ToListAsync();
            return View(pizzas);
        }
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var pizza = _context.Pizzas.FirstOrDefault(p => p.id == id);
            if (pizza != null)
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
                var cartItem = cart.FirstOrDefault(c => c.Pizza.id == id);
                if (cartItem == null)
                {
                    cart.Add(new CartItem { Pizza = pizza, Quantity = 1 });
                }
                else
                {
                    cartItem.Quantity++;
                }
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.Pizza.id == id);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Cart");
        }

    public IActionResult PlaceOrder()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        // Создание нового заказа
        var order = new Orders
        {
            dateOrder = DateTime.Now,
            status = "Ready", // Устанавливаем статус заказа, например, "Новый"
            idUser = CurrentUser.user.id // Укажите ID пользователя, который делает заказ
        };
        _context.Orders.Add(order);
        _context.SaveChanges();

        // Добавление пицц в заказ
        foreach (var item in cart)
        {
            _context.OrderPizzas.Add(new OrderPizzas
            {
                idPizza = item.Pizza.id,
                idOrder = order.id,
                count = item.Quantity
            });
        }
        _context.SaveChanges();

        // Очистка корзины после оформления заказа
        HttpContext.Session.Remove("Cart");

        return RedirectToAction("Index");
    }
        
    }
}

