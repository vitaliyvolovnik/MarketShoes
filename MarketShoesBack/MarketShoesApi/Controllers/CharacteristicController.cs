using BLL.Services;
using DLL.Models;
using DLL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketShoesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicController : ControllerBase
    {

        private readonly SellerService _sellerService;

        public CharacteristicController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }


        [AllowAnonymous]
        [HttpGet(Name = "GetAllCharacteristics")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sellerService.GetAllCharacteristicsAsync());
        }


        [AllowAnonymous]
        [HttpGet("{characteristicName}", Name = "GetCharacteristicBySub")]
        public async Task<IActionResult> Get([FromRoute] string characteristicName)
        {
            return Ok(await _sellerService.GetCharacteristicAsync(characteristicName));
        }

        [Authorize(Roles ="Admin")]
        [HttpPost(Name = "CreateChareacteristic")]
        public async Task<IActionResult> Creaet([FromBody] Characteristic characteristic)
        {
            var created = await _sellerService.CreateAsync(characteristic);
            if (created == null)
                return BadRequest();
            return Ok(created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}", Name = "UpdateChareacteristic")]
        public async Task<IActionResult> Update([FromBody] Characteristic characteristic, [FromRoute] int id)
        {
            var updated = await _sellerService.UpdateAsync(characteristic,id);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }





    }
}
