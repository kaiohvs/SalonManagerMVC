using Microsoft.EntityFrameworkCore;
using SalonManager.Data.Context;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces;

namespace SalonManager.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<IEnumerable<User>> FindAll()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<User?> FindById(int id)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> RemoverCustomer(int id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return false;
            }
            else
            {
                _appDbContext.Users.Remove(user);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
        }
        public async Task Save(User entity)
        {
            var existingEntity =  await _appDbContext.Users.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                _appDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
                _appDbContext.Users.Add(entity);

             await _appDbContext.SaveChangesAsync();
        }
    }
}
