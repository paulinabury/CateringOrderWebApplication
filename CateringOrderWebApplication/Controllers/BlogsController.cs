using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
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
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogsController(IBlogPostRepository blogPostRepository
            , IBlogPostLikeRepository blogPostLikeRepository
            , IBlogPostCommentRepository blogPostCommentRepository
            , SignInManager<IdentityUser> signInManager
            , UserManager<IdentityUser> userManager
        )
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
            _blogPostCommentRepository = blogPostCommentRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPost = await _blogPostRepository.GetAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();

            if (blogPost != null)
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                if (_signInManager.IsSignedIn(User))
                {
                    // Get like for this blog for this user
                    var likesForBlog = await _blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

                    var userId = _userManager.GetUserId(User);

                    if (userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                }

                // Get comments for blog post
                var blogCommentsDomainModel = await _blogPostCommentRepository.GetCommentsByBlogIdAsync(blogPost.Id);

                var blogCommentsForView = new List<BlogComment>();

                foreach (var blogComment in blogCommentsDomainModel)
                {
                    var username = (await _userManager.FindByIdAsync(blogComment.UserId.ToString()))?.UserName;

                    blogCommentsForView.Add(new BlogComment
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        Username = username ?? "user name",
                    });
                }

                blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Contet = blogPost.Contet,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    IsVisible = blogPost.IsVisible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked,
                    Comments = blogCommentsForView
                };
            }

            return View(blogDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel model)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var domainModel = new BlogPostComment
                {
                    BlogPostId = model.Id,
                    Description = model.CommentDescription,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    DateAdded = DateTime.Now,
                };

                await _blogPostCommentRepository.AddAsync(domainModel);

                return RedirectToAction("Index"
                    , "Blogs"
                    , new { urlHandle = model.UrlHandle }
                );
            }

            return View();
        }
    }
}