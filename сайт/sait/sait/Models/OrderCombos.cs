namespace sait.Models
{
    public class OrderCombos
    {
        public int Id { get; set; }
        public int IdCombo { get; set; }
        public int IdOrder { get; set; }
        public int Count { get; set; }

        public Combos Combo { get; set; }
        public Orders Order { get; set; }
    }
}
