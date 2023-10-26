using Microsoft.AspNetCore.Identity;

namespace CateringOrderWebApplication.Repositories.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}