using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Infrastructure.ViewModels.ShelterDTO;
using Application.IService;

namespace PRN231_PetCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterService _shelterService;

        public ShelterController(IShelterService shelterService)
        {
            _shelterService = shelterService;
        }

        // GET: api/shelter
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _shelterService.GetAll(pageNumber, pageSize);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        // GET: api/shelter/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _shelterService.GetById(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        // POST: api/shelter
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShelterReqDTO createForm)
        {
            var result = await _shelterService.Create(createForm);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.ShelterId }, result);
        }

        // PUT: api/shelter/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ShelterReqDTO updateForm)
        {
            var result = await _shelterService.Update(updateForm, id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        // DELETE: api/shelter/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _shelterService.Delete(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
    }
}
