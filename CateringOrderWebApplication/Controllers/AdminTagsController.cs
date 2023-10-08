using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels;
using CateringOrderWebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CateringOrderWebApplication.Controllers
{
	public class AdminTagsController : Controller
	{
		private readonly CateringOrderDbContext _dbContext;

		public AdminTagsController(CateringOrderDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult AddTag()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddTag(AddTagRequest addRequest)
		{
			// mapping AddTagRequest to domain model Tag
			var newTag = new Tag
			{
				Name = addRequest.Name,
				DisplayName = addRequest.DisplayName,
			};

			await _dbContext.Tags.AddAsync(newTag);
			await _dbContext.SaveChangesAsync();

			return RedirectToAction("GetAll");
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var tags = _dbContext.Tags.ToList();
			return View(tags);
		}

		[HttpGet]
		public IActionResult Edit(Guid id)
		{
			var tag = _dbContext.Tags.FirstOrDefault(t => t.Id == id);
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
		public IActionResult Edit(EditTagRequest request)
		{
			var tag = new Tag
			{
				Id = request.Id,
				Name = request.Name,
				DisplayName = request.DisplayName,
			};

			var existingTag = _dbContext.Tags.Find(tag.Id);

			// Show error notification
			if (existingTag == null) return RedirectToAction("Edit", new { id = request.Id });

			existingTag.Name = tag.Name;
			existingTag.DisplayName = tag.DisplayName;
			_dbContext.SaveChanges();

			// Show success notification
			return RedirectToAction("Edit", new { id = request.Id });
		}

		[HttpPost]
		public IActionResult Delete(EditTagRequest request)
		{
			var id = request.Id;
			var existingTag = _dbContext.Tags.Find(id);
			if (existingTag != null)
			{
				_dbContext.Tags.Remove(existingTag);
				_dbContext.SaveChanges();

				// show a success notification
				return RedirectToAction("GetAll");
			}

			// show error notification
			return RedirectToAction("Edit", new { id = request.Id });
		}
	}
}