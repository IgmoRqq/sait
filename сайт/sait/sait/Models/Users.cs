using System;

namespace sait.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }

        public Roles Role { get; set; }
    }
}