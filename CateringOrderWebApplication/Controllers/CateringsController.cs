using CateringOrderWebApplication.Models.DomainModels.Caterings;
using CateringOrderWebApplication.Models.DomainModels.Tags;
using CateringOrderWebApplication.Models.ViewModels.Caterings;
using CateringOrderWebApplication.Repositories.Caterings;
using CateringOrderWebApplication.Repositories.Tags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CateringOrderWebApplication.Controllers
{
    public class CateringsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly ICateringRepository _cateringRepository;

        public CateringsController(ICateringRepository cateringRepository, ITagRepository tagRepository)
        {
            _cateringRepository = cateringRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AddCatering()
        {
            //get tags from repository
            var tags = await _tagRepository.GetAllAsync();

            var model = new AddCateringRequest
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
        public async Task<IActionResult> AddCatering(AddCateringRequest request)
        {
            var catering = new Catering
            {
                Name = request.Name,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                Price = request.Price,
                FeaturedImageUrl = request.FeaturedImageUrl,
            };

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

            catering.Tags = selectedTags;

            await _cateringRepository.AddAsync(catering);
            return RedirectToAction("AddCatering");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var caterings = await _cateringRepository.GetAllAsync(c => c.Tags);
            return View(caterings);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var catering = await _cateringRepository.GetByIdAsync(id, c => c.Tags);
            var tagsDomainModel = await _tagRepository.GetAllAsync();

            if (catering != null)
            {
                var viewModel = new EditCateringRequest
                {
                    Id = catering.Id,
                    Name = catering.Name,
                    ShortDescription = catering.ShortDescription,
                    Content = catering.Content,
                    Price = catering.Price,
                    FeaturedImageUrl = catering.FeaturedImageUrl,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = catering.Tags.Select(x => x.Id.ToString()).ToArray(),
                };

                return View(viewModel);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCateringRequest request)
        {
            var catering = new Catering
            {
                Id = request.Id,
                Name = request.Name,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                Price = request.Price,
                FeaturedImageUrl = request.FeaturedImageUrl,
            };

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

            catering.Tags = selectedTags;

            var updatedCatering = await _cateringRepository.UpdateAsync(request.Id, catering);
            if (updatedCatering != null)
            {
                // show success notification
                return RedirectToAction("Edit");
            }

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditCateringRequest request)
        {
            var deletedCatering = await _cateringRepository.DeleteAsync(request.Id);
            if (deletedCatering != null)
            {
                //show success notification
                return RedirectToAction("GetAll");
            }

            // show error notification
            return RedirectToAction("Edit", new { id = request.Id });
        }
    }
}