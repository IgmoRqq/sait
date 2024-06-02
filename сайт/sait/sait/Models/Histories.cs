using System.ComponentModel.DataAnnotations.Schema;

namespace sait.Models
{
    public class Histories
    {
        public int id { get; set; }
        [ForeignKey("Orders")]
        public int idOrder { get; set; }
        [ForeignKey("Users")]
        public int idUser { get; set; }

    }
}
