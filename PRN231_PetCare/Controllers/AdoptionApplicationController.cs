using Application.IService;
using Infrastructure.ViewModels.AdoptionApplicationDTO;
using Microsoft.AspNetCore.Mvc;

namespace PRN231_PetCare.Controllers
{
	[ApiController]
	[Route("api/adoption-applications")]
	public class AdoptionApplicationController : ControllerBase
	{
		private readonly IAdoptionApplicationService _service;
		private readonly ILogger<AdoptionApplicationController> _logger;

		public AdoptionApplicationController(IAdoptionApplicationService service, ILogger<AdoptionApplicationController> logger)
		{
			_service = service;
			_logger = logger;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
		{
			var response = await _service.GetAllApplications(pageNumber, pageSize);
			if (response == null) return NotFound();

			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var response = await _service.GetApplicationById(id);
			if (response == null) return NotFound();

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAdoptionApplication([FromBody] AdoptionApplicationReq req)
		{
			if (req == null) return BadRequest("Body is null");
			if (req.CatId <= 0 || req.AdopterId <= 0) return BadRequest("Invalid cat ID and adopter ID.");

			var response = await _service.CreateApplication(req);
			if (response == null) return BadRequest();

			return response.Success ? Ok() : BadRequest(response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAdoptionApplication(int id, [FromBody] AdoptionApplicationReq req)
		{
			if (id <= 0) return BadRequest("Invalid ID.");
			if (req == null) return BadRequest("Body is null");
			if (req.CatId <= 0 || req.AdopterId <= 0) return BadRequest("Invalid cat ID and adopter ID.");

			var response = await _service.UpdateApplication(req, id);
			if (response == null) return NotFound();

			return Ok(response);
		}

		[HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveAdoptionApplication(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var response = await _service.ApproveAdoptionApplication(id);
            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAdoptionApplication(int id)
		{
			if (id <= 0) return BadRequest("Invalid ID.");

			var response = await _service.RemoveApplication(id);
			if (response == null) return BadRequest(response); 
			
			return Ok(response);
		}
	}
}
