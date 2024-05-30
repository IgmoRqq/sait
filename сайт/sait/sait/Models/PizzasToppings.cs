namespace sait.Models
{
    public class PizzasToppings
    {
        public int Id { get; set; }
        public int IdPizza { get; set; }
        public int IdTopping { get; set; }

        public Pizzas Pizza { get; set; }
        public Toppings Topping { get; set; }
    }
}
