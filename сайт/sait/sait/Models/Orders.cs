using System;

namespace sait.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public string Status { get; set; }
        public int IdUser { get; set; }

        public Users User { get; set; }
    }
}
