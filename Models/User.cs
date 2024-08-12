using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Akij_Bashir_It.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public ICollection<UserProduct> UserProducts { get; set; }
    }
}
