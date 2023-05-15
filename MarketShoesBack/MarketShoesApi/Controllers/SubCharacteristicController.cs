using BLL.Services;
using DLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketShoesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCharacteristicController : ControllerBase
    {
        private readonly SellerService _sellerService;

        public SubCharacteristicController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }


        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetSubCharacteristicByid")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _sellerService.GetSubCharacteristicAsync(id));
        }

        [AllowAnonymous]
        [HttpGet("characteristic/{characteristicId}", Name = "GetSubByCharacteristicId")]
        public async Task<IActionResult> GetByCharacteristicId([FromRoute] int characteristicId)
        {
            return Ok(await _sellerService.GetSubCharacteristicsAsync(characteristicId));
        }



        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "CreateSubChareacteristic")]
        public async Task<IActionResult> Creaet([FromBody] SubCharacteristic characteristic)
        {
            var created = await _sellerService.CreateAsync(characteristic);
            if (created == null)
                return BadRequest();
            return Ok(created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}", Name = "UpdateSubChareacteristic")]
        public async Task<IActionResult> Update([FromBody] SubCharacteristic characteristic, [FromRoute] int id)
        {
            var updated = await _sellerService.UpdateAsync(characteristic,id);
            if(updated == null)
                return NotFound();
            return Ok(updated);
        }



    }
}
