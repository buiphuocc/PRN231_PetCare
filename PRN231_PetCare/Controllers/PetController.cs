using Application.IService;
using Application.Services;
using Infrastructure.ViewModels.CatDTO;
using Microsoft.AspNetCore.Mvc;

namespace PRN231_PetCare.Controllers;


[Route("api/cats")]
[ApiController]
public class PetController : ControllerBase
{
    private readonly ICatService _catService;

    public PetController(ICatService catService)
    {
        _catService = catService;
    }

    //[Authorize(Roles = "Staff")]
    [HttpGet]
    public async Task<IActionResult> GetCat([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var result = await _catService.GetAll(page, pageSize);
        return result.Success ? Ok(result) : BadRequest(result);
    }


    //[Authorize(Roles = "Staff")]
    [HttpPost]
    public async Task<IActionResult> CreateCat([FromBody] CatReqDTO form)
    {
        var createForm = new CatReqDTO();

        var result = await _catService.Create(createForm);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    // [Authorize(Roles = "Staff")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCatlById(int id)
    {
        var result = await _catService.GetById(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    //[Authorize(Roles = "Staff")]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditCat(int id, [FromBody] CatReqDTO form)
    {
        var result = await _catService.Update(form, id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    //[Authorize(Roles = "Staff")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCat(int id)
    {
        var result = await _catService.Delete(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
