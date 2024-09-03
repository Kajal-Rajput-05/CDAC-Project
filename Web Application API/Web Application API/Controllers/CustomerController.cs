using Application.Domain;
using Application.Service;
using Application.Service.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Application_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetEventList(int UserId)
        {
            try
            {
                var response = await _customerService.GetEventList(UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> Payment([FromQuery] int id, PaymentType paymentType)
        {
            try
            {
                var response = await _customerService.Payment(new Booking()
                {
                    Id = id,
                    PaymentType = paymentType,
                });
                return Ok(new { data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetAllEventList()
        {
            try
            {
                var response = await _customerService.GetEventList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> CreateBooking(Booking request)
        {
            try
            {
                var response = await _customerService.CreateBooking(request);
                return Ok(new { data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetBookingListByUser(int UserId)
        {
            try
            {
                var response = await _customerService.GetBookingListByUser(UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> DeleteBooking([FromQuery] int Id, Status Status)
        {
            try
            {
                var response = await _customerService.DeleteBooking(Id, Status);
                return Ok(new { data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetFilterEventList([FromQuery] ArtistType artistType)
        {
            try
            {
                var response = await _customerService.GetFilterEventList(artistType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
