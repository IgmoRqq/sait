namespace sait.Models
{
    public class OrderPizzas
    {
        public int Id { get; set; }
        public int IdPizza { get; set; }
        public int IdOrder { get; set; }
        public int Count { get; set; }

        public Pizzas Pizza { get; set; }
        public Orders Order { get; set; }
    }
}
