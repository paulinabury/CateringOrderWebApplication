using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Models.DomainModels.Tags;
using CateringOrderWebApplication.Models.ViewModels.BlogPosts;
using CateringOrderWebApplication.Repositories.BlogPosts;
using CateringOrderWebApplication.Repositories.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CateringOrderWebApplication.Controllers;

[Authorize(Roles = "Admin, SuperAdmin")]
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
            var existingTag = await _tagRepository.GetByIdAsync(selectedTagIdGuid);
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
        var blogPosts = await _blogPostRepository.GetAllAsync(t => t.Tags, c => c.Comments);
        return View(blogPosts);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        // Get the result from repo
        var blogPost = await _blogPostRepository.GetByIdAsync(id, p => p.Tags, c => c.Comments);
        var tagsDomainModel = await _tagRepository.GetAllAsync();
        if (blogPost != null)
        {
            //map domain model to the view model
            var viewModel = new EditBlogPostRequest
            {
                Id = blogPost.Id,
                Heading = blogPost.Heading,
                PageTitle = blogPost.PageTitle,
                Contet = blogPost.Contet,
                Author = blogPost.Author,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                IsVisible = blogPost.IsVisible,
                Tags = tagsDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray(),
            };

            return View(viewModel);
        }

        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditBlogPostRequest request)
    {
        // map to domain model
        var blogPost = new BlogPost
        {
            Id = request.Id,
            Heading = request.Heading,
            PageTitle = request.PageTitle,
            Contet = request.Contet,
            Author = request.Author,
            FeaturedImageUrl = request.FeaturedImageUrl,
            UrlHandle = request.UrlHandle,
            PublishedDate = request.PublishedDate,
            ShortDescription = request.ShortDescription,
            IsVisible = request.IsVisible,
        };

        // map tags to domain model
        var selectedTags = new List<Tag>();
        foreach (var selectedTagId in request.SelectedTags)
        {
            if (Guid.TryParse(selectedTagId, out var tagId))
            {
                var tag = await _tagRepository.GetByIdAsync(tagId);
                if (tag != null)
                {
                    selectedTags.Add(tag);
                }
            }
        }

        blogPost.Tags = selectedTags;

        // submit to repository
        var updatedBlogPost = await _blogPostRepository.UpdateAsync(request.Id, blogPost);
        if (updatedBlogPost != null)
        {
            // show success notification
            return RedirectToAction("Edit");
        }

        // show error notigication
        return RedirectToAction("Edit");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditBlogPostRequest request)
    {
        var deletedBlogPost = await _blogPostRepository.DeleteAsync(request.Id);
        if (deletedBlogPost != null)
        {
            //show success notification
            return RedirectToAction("GetAll");
        }

        // show error notification
        return RedirectToAction("Edit", new { id = request.Id });
    }
}