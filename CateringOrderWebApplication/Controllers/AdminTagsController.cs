using CateringOrderWebApplication.Models.DomainModels.Tags;
using CateringOrderWebApplication.Models.ViewModels.Tags;
using CateringOrderWebApplication.Repositories.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CateringOrderWebApplication.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult AddTag()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTag(AddTagRequest addRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // mapping AddTagRequest to domain model Tag
            var newTag = new Tag
            {
                Name = addRequest.Name,
                DisplayName = addRequest.DisplayName,
            };

            await _tagRepository.AddAsync(newTag);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);

            if (tag == null) return View(null);

            var editTagRequest = new EditTagRequest
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName,
            };

            return View(editTagRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest request)
        {
            var tag = new Tag
            {
                Id = request.Id,
                Name = request.Name,
                DisplayName = request.DisplayName,
            };

            var updatedTag = await _tagRepository.EditAsync(tag);
            if (updatedTag != null)
            {
                // show success notification
            }
            else
            {
                // show error notification
            }

            return RedirectToAction("Edit", new { id = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest request)
        {
            var existingTag = await _tagRepository.DeleteAsync(request.Id);

            if (existingTag != null)
            {
                // show a success notification
                return RedirectToAction("GetAll");
            }

            // show error notification
            return RedirectToAction("Edit", new { id = request.Id });
        }
    }
}