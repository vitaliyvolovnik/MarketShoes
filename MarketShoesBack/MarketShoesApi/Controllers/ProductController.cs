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
    public class ProductController : ControllerBase
    {
        private readonly SellerService _sellerService;

        public ProductController(SellerService sellerSevice)
        {
            _sellerService = sellerSevice;  
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sellerService.GetAllProductsAsync());
        }

        [AllowAnonymous]
        [HttpGet("{id}",Name = "GetProduct")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            return Ok(await _sellerService.GetProductsAsync(id));
        }

        [AllowAnonymous]
        [HttpGet("Seller/{sellerId}" ,Name = "GetProductsBySeller")]
        public async Task<IActionResult> GetBySellerId([FromRoute] int sellerId)
        {
            return Ok(await _sellerService.GetProductsAsync(sellerId));
        }

        [Authorize(Roles ="Seller")]
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            var IdClaim = User.Claims.FirstOrDefault(c => c.Type == "SellerCustomerIdClime");
            var sellerId = 0;
            int.TryParse(IdClaim?.Value, out sellerId);

            return Ok(await _sellerService.CreateAsync(product, sellerId));
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("ChangeSaleStatus/{productId}/{status}", Name = "ChangeSaleStatus")]
        public async Task<IActionResult> ChangeSaleStatus([FromRoute] int productId, [FromRoute] bool status)
        {
            var product = await _sellerService.ChangeSaleStatusAsync(productId, status);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Product product)
        {
            var newProduct = await _sellerService.UpdateAsync(product, id);
            if (newProduct == null)
                return NotFound();
            return Ok(newProduct);
        }




    }
}
