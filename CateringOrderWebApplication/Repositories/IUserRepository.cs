using Microsoft.AspNetCore.Identity;

namespace CateringOrderWebApplication.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}