using CateringOrderWebApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _dbContext;

        public UserRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await _dbContext.Users.ToListAsync();
            var superAdminUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@cateringOrderWebApp.com");

            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }
            return users;
        }
    }
}