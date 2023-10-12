using CateringOrderWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CateringOrderWebApplication.Models.ViewModels;
using CateringOrderWebApplication.Repositories;

namespace CateringOrderWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger
        , IBlogPostRepository blogPostRepository
        , ITagRepository tagRepository)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            // get all blogs
            var existingBlogs = await _blogPostRepository.GetAllAsync();

            // get all tags
            var tags = await _tagRepository.GetAllAsync();

            var model = new HomeViewModel()
            {
                BlogPosts = existingBlogs,
                Tags = tags,
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}