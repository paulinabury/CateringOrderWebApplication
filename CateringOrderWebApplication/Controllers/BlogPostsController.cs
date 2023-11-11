using CateringOrderWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CateringOrderWebApplication.Models.ViewModels;
using CateringOrderWebApplication.Repositories.BlogPosts;
using CateringOrderWebApplication.Repositories.Caterings;
using CateringOrderWebApplication.Repositories.Tags;

namespace CateringOrderWebApplication.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICateringRepository _cateringRepository;

        public BlogPostsController(ILogger<HomeController> logger
        , IBlogPostRepository blogPostRepository
        , ITagRepository tagRepository, ICateringRepository cateringRepository)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _tagRepository = tagRepository;
            _cateringRepository = cateringRepository;
        }

        public async Task<IActionResult> Index()
        {
            // get all blogs
            var existingBlogs = await _blogPostRepository.GetAllAsync(t => t.Tags, c => c.Comments);

            // get all tags
            var tags = await _tagRepository.GetAllAsync();

            // get all caterings
            var existingCaterings = await _cateringRepository.GetAllAsync(c => c.Tags);

            var model = new HomeViewModel()
            {
                BlogPosts = existingBlogs,
                Tags = tags,
                Caterings = existingCaterings,
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}