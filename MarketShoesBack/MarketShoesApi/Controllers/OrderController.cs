using BLL.Services;
using DLL.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketShoesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly SellerService _sellerService;

        public OrderController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sellerService.GetOrdersAsync());
        }

        [Authorize]
        [HttpGet("customerOrders",Name = "GetByCustomerId")]
        public async Task<IActionResult> Get()
        {

            var IdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = 0;
            int.TryParse(IdClaim?.Value, out userId);

            return Ok(await _sellerService.GetOrdersByCustomerAsync(userId));
        }

        [Authorize]
        [HttpGet("sellerOrders/{sellerId}", Name = "GetBySellerId")]
        public async Task<IActionResult> Get([FromBody] int sellerId)
        {
            return Ok(await _sellerService.GetOrdersBySellerAsync(sellerId));
        }

        [Authorize]
        [HttpPost("", Name = "CreateByCustomer")]
        public async Task<IActionResult> Create()
        {
            var IdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = 0;
            int.TryParse(IdClaim?.Value, out userId);
            return Ok(await _sellerService.CreateOrdersAsync(userId));
        }

        [Authorize]
        [HttpPatch("{id}/{OrderStatus}", Name = "ChangeStatu")]
        public async Task<IActionResult> ChangeState([FromRoute] int id, [FromRoute] OrderStatus orderStatus)
        {
            var order = await _sellerService.ChangeOrderState(id, orderStatus);
            if (order == null)
                return BadRequest();
            return Ok(order);
        }
    }
}
