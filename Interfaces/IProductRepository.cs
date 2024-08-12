using Akij_Bashir_It.Models;

namespace Akij_Bashir_It.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product>GetProducts();
        Product GetProduct(int id);
        ICollection<User> GetUserByProduct(int productId);

        bool ProductExistis(int productId);

        bool CreateProduct(Product product, int userId);
        bool UpdateProduct(Product product, int userId);
        bool DeleteProduct(Product product);
        bool Save();
    }
}
