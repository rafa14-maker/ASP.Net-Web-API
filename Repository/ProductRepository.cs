using Akij_Bashir_It.Data;
using Akij_Bashir_It.Interfaces;
using Akij_Bashir_It.Models;
using Microsoft.EntityFrameworkCore;

namespace Akij_Bashir_It.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public Product GetProduct(int id)
        {
            return _context.Products.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public ICollection<User> GetUserByProduct(int productId)
        {
            return _context.UserProducts.Where(p=> p.ProductId == productId).Select(p => p.User).ToList();   
        }

        public bool CreateProduct(Product product, int userId)
        {
            var userProductEntity = _context.Users.Where(a=>a.Id == userId).FirstOrDefault();

            var userProduct = new UserProduct()
            {
                User = userProductEntity,
                Product = product,
            };

            _context.Add(userProduct);

            _context.Add(product);

            return Save();
        }

        public bool UpdateProduct(Product product, int userId)
        {
            _context.Update(product);
            return Save();
        }
        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);
            return Save();
        }
        public bool ProductExistis(int productId)
        {
            return _context.Products.Any(c=> c.Id == productId);    
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0?true:false;
        }
    }
}
