using Application.IService;
using Application.Services;
using Infrastructure.ViewModels.CatDTO;
using Infrastructure.ViewModels.DonationDTO;
using Microsoft.AspNetCore.Mvc;

namespace PRN231_PetCare.Controllers;


[Route("api/donation")]
[ApiController]
public class DonationController : ControllerBase
{
    private readonly IDonationService _service;

    public DonationController(IDonationService service)
    {
        _service = service;
    }

    //[Authorize(Roles = "Staff")]
    [HttpGet]
    public async Task<IActionResult> Getdonation([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var result = await _service.GetAll(page, pageSize);
        return result.Success ? Ok(result) : BadRequest(result);
    }


    //[Authorize(Roles = "Staff")]
    [HttpPost]
    public async Task<IActionResult> CreateDonation([FromBody] DonationReqDTO form)
    {
        if (form == null) // Check if the incoming form is null
        {
            return BadRequest("Request body is null");
        }

        var result = await _service.Create(form); // Use the incoming DTO

        return result.Success ? Ok(result) : BadRequest(result);
    }


    // [Authorize(Roles = "Staff")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDonationById(int id)
    {
        var result = await _service.GetById(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    //[Authorize(Roles = "Staff")]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditDonation(int id, [FromBody] DonationReqDTO form)
    {
        var result = await _service.Update(form, id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    //[Authorize(Roles = "Staff")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonation(int id)
    {
        var result = await _service.Delete(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
