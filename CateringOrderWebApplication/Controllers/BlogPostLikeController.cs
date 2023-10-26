using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Models.ViewModels.BlogPosts;
using CateringOrderWebApplication.Repositories.BlogPosts;
using Microsoft.AspNetCore.Mvc;

namespace CateringOrderWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest request)
        {
            var blogPostLikeModel = new BlogPostLike
            {
                BlogPostId = request.BlogPostId,
                UserId = request.UserId,
            };

            await _blogPostLikeRepository.AddLikeForBlog(blogPostLikeModel);

            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPostId);
            return Ok(totalLikes);
        }
    }
}