using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Models.DomainModels.Tags;
using CateringOrderWebApplication.Models.ViewModels.BlogPosts;
using CateringOrderWebApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CateringOrderWebApplication.Controllers;

public class AdminBlogPostController : Controller
{
    private readonly ITagRepository _tagRepository;
    private readonly IBlogPostRepository _blogPostRepository;

    public AdminBlogPostController(ITagRepository tagRepository
        , IBlogPostRepository blogPostRepository
    )
    {
        _tagRepository = tagRepository;
        _blogPostRepository = blogPostRepository;
    }

    [HttpGet]
    public async Task<IActionResult> AddBlogPost()
    {
        //get tags from repository
        var tags = await _tagRepository.GetAllAsync();

        var model = new AddBlogPostRequest
        {
            Tags = tags.Select(x => new SelectListItem
            {
                Text = x.DisplayName,
                Value = x.Id.ToString()
            })
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddBlogPost(AddBlogPostRequest request)
    {
        var blogPost = new BlogPost
        {
            Heading = request.Heading,
            PageTitle = request.PageTitle,
            Contet = request.Contet,
            ShortDescription = request.ShortDescription,
            FeaturedImageUrl = request.FeaturedImageUrl,
            UrlHandle = request.UrlHandle,
            PublishedDate = request.PublishedDate,
            Author = request.Author,
            IsVisible = request.IsVisible,
        };

        // Map Domain model tags from selected tags
        var selectedTags = new List<Tag>();

        foreach (var selectedTagId in request.SelectedTags)
        {
            var selectedTagIdGuid = Guid.Parse(selectedTagId);
            var existingTag = await _tagRepository.GetAsync(selectedTagIdGuid);
            if (existingTag != null)
            {
                selectedTags.Add(existingTag);
            }
        }

        blogPost.Tags = selectedTags;

        await _blogPostRepository.AddAsync(blogPost);
        return RedirectToAction("AddBlogPost");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var blogPosts = await _blogPostRepository.GetAllAsync();
        return View(blogPosts);
    }
}