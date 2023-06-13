using BLL.Services;
using BLL.ViewModels;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IUrlHelper _urlHelper;
        public ProductController(SellerService sellerSevice, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IUrlHelper urlHelper)
        {
            _sellerService = sellerSevice;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _urlHelper = urlHelper;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            var products = (await _sellerService.GetAllProductsAsync()).ToList();

            var baseUrl = $"{Request.Scheme}://{Request.Host}";


            products.ForEach(p => p.Photos.ForEach(ph =>
            {
                var imagePath = _urlHelper.Content($"~/uploads/{ph.Path}");
                ph.Path = $"{baseUrl}{imagePath}";
            }));

            return Ok(products);
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

        [Authorize(Roles ="SELLER")]
        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] FormDataModel<Product> productModel)
        {
            var IdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = 0;
            int.TryParse(IdClaim?.Value, out userId);
                

            var product = productModel.GetEntity();
            product.Characteristics = product.Characteristics.Where(x => x != null).ToList();
            product.Code = "2";
            product.Photos.Clear();
            product.SellerId = userId;
            foreach (var file in productModel.Photos)
            {
                if (file.Length > 0)
                {
                    var photo = new Photo { Path = await SaveFile(file) };
                    product.Photos.Add(photo);
                }
            }

            return Ok(await _sellerService.CreateAsync(product, userId));
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



        private async Task<string> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, $"uploads/{fileName}");

            try
            {

                Directory.CreateDirectory("uploads");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




    }
}
