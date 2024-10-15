using Application.IService;
using Application.Services;
using Infrastructure.ViewModels.CatDTO;
using Infrastructure.ViewModels.CatProfileDTO;
using Microsoft.AspNetCore.Mvc;

namespace PRN231_PetCare.Controllers;


[Route("api/catProfiles")]
[ApiController]
public class CatProfileController : ControllerBase
{
    private readonly ICatProfileService _catService;

    public CatProfileController(ICatProfileService catService)
    {
        _catService = catService;
    }

    //[Authorize(Roles = "Staff")]
    [HttpGet]
    public async Task<IActionResult> GetCatProfile([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var result = await _catService.GetAll(page, pageSize);
        return result.Success ? Ok(result) : BadRequest(result);
    }


    //[Authorize(Roles = "Staff")]
    [HttpPost]
    public async Task<IActionResult> CreateCatProfile([FromBody] CatProfileReqDTO form)
    {
        if (form == null) // Check if the incoming form is null
        {
            return BadRequest("Request body is null");
        }

        var result = await _catService.Create(form); // Use the incoming DTO

        return result.Success ? Ok(result) : BadRequest(result);
    }


    // [Authorize(Roles = "Staff")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCatProfileById(int id)
    {
        var result = await _catService.GetById(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    //[Authorize(Roles = "Staff")]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditCatProfile(int id, [FromBody] CatProfileReqDTO form)
    {
        var result = await _catService.Update(form, id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    //[Authorize(Roles = "Staff")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCatProfile(int id)
    {
        var result = await _catService.Delete(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
