using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Akij_Bashir_It.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public ICollection<UserProduct> UserProducts { get; set; }
    }
}
