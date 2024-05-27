using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Save(User entity);
        Task<User?> FindById(int id);
        Task<IEnumerable<User>> FindAll();
        Task<bool> RemoverCustomer(int id);
    }
}
