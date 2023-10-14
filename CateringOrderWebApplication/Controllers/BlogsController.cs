using CateringOrderWebApplication.Models.ViewModels.BlogPosts;
using CateringOrderWebApplication.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CateringOrderWebApplication.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogsController(IBlogPostRepository blogPostRepository
            , IBlogPostLikeRepository blogPostLikeRepository
            , SignInManager<IdentityUser> signInManager
            , UserManager<IdentityUser> userManager
        )
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var existingBlogPost = await _blogPostRepository.GetAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();

            if (existingBlogPost != null)
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikes(existingBlogPost.Id);

                if (_signInManager.IsSignedIn(User))
                {
                    // check if user has liked the blog post
                    var likesForBlog = await _blogPostLikeRepository
                        .GetLikesForBlog(existingBlogPost.Id);
                    var userId = _userManager.GetUserId(User);
                    if (userId != null)
                    {
                        var userLike = likesForBlog
                            .FirstOrDefault(x => x.UserId == Guid.Parse(userId));

                        liked = userLike != null;
                    }
                }

                blogDetailsViewModel = new BlogDetailsViewModel()
                {
                    Id = existingBlogPost.Id,
                    Contet = existingBlogPost.Contet,
                    PageTitle = existingBlogPost.PageTitle,
                    Author = existingBlogPost.Author,
                    FeaturedImageUrl = existingBlogPost.FeaturedImageUrl,
                    Heading = existingBlogPost.Heading,
                    PublishedDate = existingBlogPost.PublishedDate,
                    ShortDescription = existingBlogPost.ShortDescription,
                    UrlHandle = existingBlogPost.UrlHandle,
                    IsVisible = existingBlogPost.IsVisible,
                    Tags = existingBlogPost.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked,
                };

                return View(blogDetailsViewModel);
            }

            return View();
        }
    }
}