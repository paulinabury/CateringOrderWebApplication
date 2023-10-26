using CateringOrderWebApplication.Models.ViewModels.Users;
using CateringOrderWebApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CateringOrderWebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _manager;

        public AdminUsersController(IUserRepository userRepository
            , UserManager<IdentityUser> manager
        )
        {
            _userRepository = userRepository;
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();
            var usersViewModel = new UserViewModel
            {
                Users = new List<User>()
            };

            foreach (var user in users)
            {
                usersViewModel.Users.Add(new User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email,
                });
            }

            return View(usersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(UserViewModel viewModel)
        {
            var newUser = new IdentityUser
            {
                UserName = viewModel.Username,
                Email = viewModel.Email,
            };

            var identityResult = await _manager.CreateAsync(newUser, viewModel.Password);

            if (identityResult != null && identityResult.Succeeded)
            {
                var roles = new List<string> { "User" };
                if (viewModel.IsAdmin)
                {
                    roles.Add("Admin");
                }

                identityResult = await _manager.AddToRolesAsync(newUser, roles);

                if (identityResult != null && identityResult.Succeeded)
                {
                    return RedirectToAction("GetAll", "AdminUsers");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _manager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var identityResult = await _manager.DeleteAsync(user);
                if (identityResult != null && identityResult.Succeeded)
                {
                    return RedirectToAction("GetAll", "AdminUsers");
                }
            }

            return View("GetAll");
        }
    }
}