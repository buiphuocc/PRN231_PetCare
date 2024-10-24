using Application.IService;
using Application.Services;
using Infrastructure.ViewModels.AdoptionContractDTO;
using Infrastructure.ViewModels.CatProfileDTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PRN231_PetCare.Controllers
{
    [Route("api/AdoptionContract")]
    [ApiController]
    public class AdoptionContractController : BaseController
    {
        private readonly IAdoptionContractService _adoptionContractService;

        public AdoptionContractController(IAdoptionContractService adoptionContractService)
        {
            _adoptionContractService = adoptionContractService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAdoptionContracts([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var adoptionContracts = await _adoptionContractService.GetAllAdoptionContractsAsync(page, pageSize);
            return Ok(adoptionContracts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdoptionContractById(int id)
        {
            var adoptionContract = await _adoptionContractService.GetAdoptionContractByIdAsync(id);
            if (adoptionContract == null)
            {
                return NotFound();
            }
            return Ok(adoptionContract);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdoptionContract([FromBody] AdoptionContractReq form)
        {
            if (form == null)
            {
                return BadRequest("Request body is null");
            }
           var result = await _adoptionContractService.CreateAdoptionContract(form);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdoptionContract(int id)
        {
            var result = await _adoptionContractService.DeleteAdoptionContract(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdoptionContract(int id, [FromBody] AdoptionContractReq form)
        {
            var result = await _adoptionContractService.UpdateAdoptionContract(form, id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}