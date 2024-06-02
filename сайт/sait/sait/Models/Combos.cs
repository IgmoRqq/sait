using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace sait.Models
{
    public class Combos
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createDate { get; set; }
        public float price { get; set; }
        [ForeignKey("Categories")]
        public int idCategory { get; set; }
    }
}
