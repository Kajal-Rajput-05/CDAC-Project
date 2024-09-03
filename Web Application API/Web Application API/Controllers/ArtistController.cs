using Application.Domain;
using Application.Service.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Application_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        [Authorize(Roles = "ARTIST")]
        public async Task<IActionResult> GetEventList(int UserId)
        {
            try
            {
                var response = await _artistService.GetEventList(UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ARTIST")]
        public async Task<IActionResult> CreateEvent(Event request)
        {
            try
            {
                var response = await _artistService.CreateEvent(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN,ARTIST")]
        public async Task<IActionResult> DeleteEvent([FromQuery] int Id, Status Status)
        {
            try
            {
                var response = await _artistService.DeleteEvent(Id, Status);
                return Ok(new { data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "ARTIST")]
        public async Task<IActionResult> UpdateEvent(Event request)
        {
            try
            {
                var response = await _artistService.UpdateEvent(request);
                return Ok(new { data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Authorize(Roles = "ARTIST")]
        public async Task<IActionResult> Payment([FromQuery] int id, PaymentType paymentType, string paymentDetail)
        {
            try
            {
                var response = await _artistService.Payment(new Event()
                {
                    Id = id,
                    PaymentType = paymentType,
                    PaymentDetail = paymentDetail.Contains("undefined") ? "" : paymentDetail
                });
                return Ok(new { data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "ARTIST")]
        public async Task<IActionResult> GetBookingListByArtist(int UserId)
        {
            try
            {
                var response = await _artistService.GetBookingListByArtist(UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
