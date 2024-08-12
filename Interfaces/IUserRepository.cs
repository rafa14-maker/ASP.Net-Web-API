using Akij_Bashir_It.Models;

namespace Akij_Bashir_It.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUser(int userId);

        ICollection<Product>GetProductByUser(int userId);

        bool UserExists(int userId);

        bool CreateUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        bool Save();

    }
}
