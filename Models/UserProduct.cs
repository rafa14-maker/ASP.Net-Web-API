using System.Reflection.Metadata.Ecma335;

namespace Akij_Bashir_It.Models
{
    public class UserProduct
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public User User { get; set; }
    }
}
