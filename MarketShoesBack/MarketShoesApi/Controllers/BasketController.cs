using BLL.Services;
using DLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketShoesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly CustomerService _customerService;

        public BasketController(CustomerService customerService)
        {
            _customerService = customerService;
        }


        [Authorize]
        [HttpPost(Name = "AddItemToBasket")]
        public async Task<IActionResult> AddItem([FromBody] BasketItem basket)
        {
            var IdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = 0;
            int.TryParse(IdClaim?.Value, out userId);

            var created = _customerService.AddToBasketAsync(basket, userId);

            if (created == null)
                return BadRequest();

            return Ok(created);

        }
        

        [Authorize]
        [HttpPut("{id}",Name = "UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] BasketItem basket, [FromRoute] int id)
        {
            var updated = _customerService.UpdateBusketElement(basket,id);

            if (updated == null)
                return BadRequest();

            return Ok(updated);

        }

        [Authorize]
        [HttpDelete("{basketItemId}", Name = "RemoveElement")]
        public async Task<IActionResult> RemoveElement([FromRoute] int basketItemId)
        {
            await _customerService.RemoveFromBasketAsync(basketItemId);

            return Ok();
        }

        [Authorize]
        [HttpDelete("clearBasket", Name = "ClearBasket")]
        public async Task<IActionResult> ClearBasket()
        {
            var IdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = 0;
            int.TryParse(IdClaim?.Value, out userId);

            await _customerService.ClearAsync(userId);

            return Ok();

        }

    }
}
