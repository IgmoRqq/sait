﻿using System.ComponentModel.DataAnnotations.Schema;

namespace sait.Models
{
    public class PizzasToppings
    {
        public int id { get; set; }
        [ForeignKey("Pizzas")]
        public int idPizza { get; set; }
        [ForeignKey("Toppings")]
        public int idTopping { get; set; }
    }
}
