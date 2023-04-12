using BLL.Services;
using BLL.ViewModels.AuthenticateModels;
using DLL.Models;
using MarketShoesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarketShoesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {

        private readonly AuthorizeService _authorizeService;
        private readonly IConfiguration _config;

        public AuthController(AuthorizeService service, IConfiguration config)
        {
            _authorizeService = service;
            _config = config;
        }


        
        [HttpPost("login",Name ="Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user =  await _authorizeService.LoginAsync(loginModel);
            if (user != null)
            {
                var token = GenerateJwt(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }

        
        [HttpPost("register",Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationModel registerationModel)
        {
            var user = await _authorizeService.Registration(registerationModel);
            if (user == null)
                return BadRequest();
            return Ok();
        }

        [Authorize]
        [HttpPost("changePassword", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel change)
        {
            if (await _authorizeService.ChangePasswordAsync(change))
                return Ok();
            return NotFound();
        }


        private string GenerateJwt(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Firstname),
                new Claim(ClaimTypes.Surname,user.Lastname),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
               _config["Jwt:Audience"],
               claims,
               expires: DateTime.Now.AddMinutes(25),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        



    }
}
