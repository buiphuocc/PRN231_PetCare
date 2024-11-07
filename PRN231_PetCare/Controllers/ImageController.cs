using Application.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PRN231_PetCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : BaseController
    {
        private readonly IImageService _imageService;


        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }



        [HttpGet]
        public async Task<IActionResult> AllImageInfors()
        {
            var result = await _imageService.GetAllImageInfors(); 
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    
    // [Authorize(Roles = "Staff,Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageInforById(int id)
        {
            var result = await _imageService.GetImageInforById(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


        //[Authorize(Roles = "Staff,Admin")]
        [HttpDelete("image/{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var result = await _imageService.DeleteImage(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file, int entityId, string entityType)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Call the service method with additional parameters
            var result = await _imageService.UploadImage(file, entityId, entityType);

            if (!result.Success)
            {
                return BadRequest(result); // Return the error response from the service
            }

            return Ok(result); // Return success response with the uploaded image info
        }



    }
}
