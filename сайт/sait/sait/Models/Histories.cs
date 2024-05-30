namespace sait.Models
{
    public class Histories
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdUser { get; set; }

        public Orders Order { get; set; }
        public Users User { get; set; }
    }
}
