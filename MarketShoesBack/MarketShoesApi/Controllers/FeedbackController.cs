using BLL.Services;
using DLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketShoesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public FeedbackController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _customerService.GetAllFeedbacksAsync());
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _customerService.GetFeedbackAsync(id));
        }

        [AllowAnonymous]
        [HttpGet("product/{productId}", Name = "GetProductFeedbacks")]
        public async Task<IActionResult> GetProductFeedbacks([FromRoute] int productId)
        {
            return Ok(await _customerService.GetProductFeedbacksAsync(productId));
        }

        [Authorize]
        [HttpGet("customer/{customerId}", Name = "GetCustomerFeedbacks")]
        public async Task<IActionResult> GetCustomerFeedbacks([FromRoute] int customerId)
        {
            return Ok(await _customerService.GetCustomerFeedbacksAsync(customerId));
        }

        [Authorize]
        [HttpPost(Name = "CreateFeedBack")]
        public async Task<IActionResult> Create([FromBody] Feedback feedback)
        {
            var IdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = 0;
            int.TryParse(IdClaim?.Value, out userId);

            var created = await _customerService.CreateAsync(feedback, userId);
            if (created == null)
                return BadRequest();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}",Name = "UpdateFeedBack")]
        public async Task<IActionResult> Update([FromBody] Feedback feedback, [FromRoute] int id)
        {
            var created = await _customerService.UpdateAsync(feedback, id);
            if (created == null)
                return NotFound();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteFeedBack")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _customerService.DeleteFeedBackAsync(id);
            return Ok();
        }
    }
}
