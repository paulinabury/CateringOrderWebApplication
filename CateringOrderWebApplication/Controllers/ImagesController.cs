using System.Net;
using CateringOrderWebApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CateringOrderWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // call a repository
            var imageUrl = await _imageRepository.UploadAsync(file);

            if (imageUrl == null)
            {
                return Problem("Image upload was not successful"
                    , null
                    , (int)HttpStatusCode.InternalServerError
                );
            }

            return new JsonResult(new { link = imageUrl });
        }
    }
}