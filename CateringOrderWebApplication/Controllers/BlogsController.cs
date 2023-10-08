using CateringOrderWebApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CateringOrderWebApplication.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var existingBlogPost = await _blogPostRepository.GetAsync(urlHandle);
            if (existingBlogPost != null)
            {
                return View(existingBlogPost);
            }

            return View();
        }
    }
}