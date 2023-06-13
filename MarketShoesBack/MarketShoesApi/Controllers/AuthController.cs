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
    public class AuthController : ControllerBase
    {

        private readonly AuthorizeService _authorizeService;
        private readonly IConfiguration _config;
        private readonly UserService _userService;

        public AuthController(AuthorizeService service, IConfiguration config, UserService userService)
        {
            _authorizeService = service;
            _config = config;
            _userService = userService;
        }



        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _authorizeService.LoginAsync(loginModel);
            if (user != null)
            {
                user.JWToken = GenerateJwt(user);
                
                return Ok(user);
            }
            return NotFound("User not found");
        }


        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationModel registerationModel)
        {
            string requestUrl = Request.Headers["Referer"];

            var user = await _authorizeService.Registration(registerationModel, requestUrl);
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

        private string GenerateJwt(UserViewModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
               _config["Jwt:Audience"],
               claims,
               expires: DateTime.Now.AddMinutes(25),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateJwt(User user)
        {
           return  GenerateJwt(new UserViewModel(user));
        }

        [HttpGet("refresh", Name = "RefreshGWT")]
        [Authorize]
        public async Task<IActionResult> Refresh()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = 0;
            int.TryParse(userIdClaim?.Value, out userId);

            var user = await _userService.GetAsync(userId);
            if (user == null)
                return NotFound();

            var jwt = GenerateJwt(user);
            return Ok(jwt);
        }

        [AllowAnonymous]
        [HttpHead("resetPass/{email}", Name = "SendEmailForReset")]
        public async Task<IActionResult> ResetPassword([FromRoute] string email)
        {
            string requestUrl = Request.Headers["Referer"];
            if (await _authorizeService.CreateResetPasswordTokenAsync(email, requestUrl))
                return Ok();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpHead("resetPass/{token}/{newPassword}", Name = "ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromRoute] string token, string newPassword)
        {
            if (await _authorizeService.ResetPasswordAsync(token, newPassword))
                return Ok();
            return BadRequest();
        }


        [AllowAnonymous]
        [HttpHead("checkEmailExist/{email}", Name = "isEmailExist")]
        public async Task<IActionResult> EmailExist([FromRoute] string email)
        {
            if (await _authorizeService.isEmailExistAsync(email))
                return Ok();
            return NotFound();
        }

        [AllowAnonymous]
        [HttpHead("confirmEmail/{token}", Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromRoute] string token)
        {
            if (await _authorizeService.ConfirmEmailAsync(token))
                return Ok();
            return NotFound();
        }



    }
}
