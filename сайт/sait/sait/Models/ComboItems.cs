namespace sait.Models
{
    public class ComboItems
    {
        public int Id { get; set; }
        public int IdCombo { get; set; }
        public int IdPizza { get; set; }

        public Combos Combo { get; set; }
        public Pizzas Pizza { get; set; }
    }
}
