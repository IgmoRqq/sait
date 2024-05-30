using System;

namespace sait.Models
{
    public class Pizzas
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int IdCategory { get; set; }
        public DateTime CreateDate { get; set; }

        public Categories Category { get; set; }
    }
}
