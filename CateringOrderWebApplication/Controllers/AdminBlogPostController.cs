using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels;
using CateringOrderWebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CateringOrderWebApplication.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly CateringOrderDbContext _dbContext;

        public AdminBlogPostController(CateringOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult AddBlogPost()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var blogPosts = _dbContext.BlogPosts.ToList();

            return View(blogPosts);
        }

        [HttpPost]
        public IActionResult AddBlogPost(AddBlogPostRequest request)
        {
            var newBlogPost = new BlogPost
            {
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                Contet = request.Contet,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = DateTime.Now,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Tags = request.Tags,
            };

            _dbContext.BlogPosts.Add(newBlogPost);
            _dbContext.SaveChanges();

            return View("AddBlogPost");
        }
    }
}