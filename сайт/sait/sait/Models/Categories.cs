using System;

namespace sait.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Pizzas> Pizza { get; set; }
    }
}
