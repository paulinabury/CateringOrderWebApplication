using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles (User, Admin, SuperAdmin)
            const string adminRoleId = "a0329a66-e162-4d14-88f8-ee80d931ac11";
            const string superAdminRoleId = "035eacd4-fd1d-496b-b857-1bcfc8438cb4";
            const string userRoleId = "02c80728-fca5-47ad-813d-e409ebaa5251";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                },

                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId,
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            // Create SuperAdmin user
            const string superAdminId = "d2630249-33c6-4090-8eb9-635473e43dd4";

            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@cateringOrderWebApp.com",
                Email = "superadmin@cateringOrderWebApp.com",
                NormalizedEmail = "superadmin@cateringOrderWebApp.com".ToUpper(),
                NormalizedUserName = "superadmin@cateringOrderWebApp.com".ToUpper(),
                Id = superAdminId,
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all roles to SuperAdmin user
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId,
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}