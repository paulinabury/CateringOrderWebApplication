﻿using CateringOrderWebApplication.Models.DomainModels.BlogPosts;

namespace CateringOrderWebApplication.Repositories.BlogPosts
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);

        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
    }
}