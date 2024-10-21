using Application.IService;
using Infrastructure.ViewModels.AppointmentDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace PRN231_PetCare.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    //[Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        [HttpPost("create-appointment")]
        public async Task<IActionResult> AddAppointment([Required] int catId, [Required] string purpose, [Required] int userId)
        {
            if (catId == null || purpose == null) BadRequest("catId and purpose required");
            try
            {
                //var userIdClaim = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
                //int.TryParse(userIdClaim, out int userId);


                var response = await _appointmentService.AddAppointment(new AppointmentDTO
                {
                    CatId = catId,
                    AppointmentDate = DateTime.UtcNow,
                    UserId = userId,
                    Purpose = purpose
                });

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new {message = ex.Message } );
            }
                
        }

        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById([Required] int appointmentId)
        {
            if (appointmentId == null) BadRequest("appointmentId required");
            try
            {


                var response = await _appointmentService.GetAppointmentById(appointmentId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("get-all-appointment")]
        public async Task<IActionResult> GetAppointments()
        {
            try
            {
                var response = await _appointmentService.GetAllAppointments();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("delete-appointment/{appointmentId}")]
        public async Task<IActionResult> DeleteAppointments([Required] int appointmentId)
        {
            try
            {
                var response = await _appointmentService.DeleteAppointmentById(appointmentId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("update-appointment")]
        public async Task<IActionResult> UpdateAppointments([FromBody] AppointmentDTO appointmentDto)
        {
            try
            {
                var response = await _appointmentService.UpdateAppointment(appointmentDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet("purposes-dropdown-option")]
        public IActionResult GetPurposeDropdownOptions()
        {
            return Ok(AppointmentPurposesString.Options);
        }
    }
}
