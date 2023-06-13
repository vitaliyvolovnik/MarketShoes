using BLL.ViewModels.AuthenticateModels;
using DLL.Models;
using DLL.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class AuthorizeService
    {

        private readonly UserRepository _userRepository;
        private readonly UserTokenRepository _userTokenRepository;
        private readonly EmailService _emailService;


        public AuthorizeService(UserRepository userRepository, EmailService emailService,UserTokenRepository userTokenRepository)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _userTokenRepository = userTokenRepository;
        }

        public async Task<User?> Registration(RegisterationModel registeration,string url)
        {
            if (!_userRepository.IsEmailExistAsync(registeration.Email).Result)
            {
                var guid = Guid.NewGuid().ToString();

                var userToken = new UserToken()
                {
                    Token = guid,
                    type = DLL.Models.Enums.TokenType.EMAIL_CONFIRMATION
                };

                var user = new User()
                {
                    Email = registeration.Email,
                    Role = registeration.Role,
                    Password = HashPassword(registeration.Password),
                    UserTokens = new List<UserToken>()
                    {
                        userToken,
                    }
                };

                var registeredUser = await _userRepository.CreateAsync(user);

                if (registeredUser != null)
                    _emailService.SendEmailConfirmationLink(guid, registeredUser.Email, url);

                return registeredUser;

            }
            else
                return null;
        }

        public async Task<UserViewModel?> LoginAsync(LoginModel login)
        {
            var user = await _userRepository.LoginAsync(login.Email, HashPassword(login.Password));
            if (user == null)
                return null;
            return new UserViewModel(user);
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordModel model)
        {
            return await _userRepository.ChangePassworndAsync(model.Email, model.Password, model.NewPassword);
        }

        public async Task<bool> ConfirmEmailAsync(string token)
        {
            var userToken = await _userTokenRepository.UseAsync(token, DLL.Models.Enums.TokenType.EMAIL_CONFIRMATION);
            if (userToken == null)
                return false;

            return await _userRepository.ConfirmEmailAsync(userToken.User.Id);
        }

        public async Task<bool> ResetPasswordAsync(string token, string password)
        {
            var userToken = await _userTokenRepository.UseAsync(token, DLL.Models.Enums.TokenType.PASSWORD_RESET);
            if (userToken == null)
                return false;

            return await _userRepository.ResetPasswordAsync(userToken.User.Id, HashPassword(password));
        }

        public async Task<bool> CreateResetPasswordTokenAsync(string email, string url)
        {

            var user = await _userRepository.FindFirstAsync(x => x.Email == email);
            if (user == null)
                return false;


            var guid = Guid.NewGuid().ToString();

            var token = new UserToken()
            {
                UserId = user.Id,
                Token = guid,
                type = DLL.Models.Enums.TokenType.PASSWORD_RESET
            };

            var created = _userTokenRepository.CreateAsync(token);
            if (created == null)
                return false;

            _emailService.SendPasswordResetEmail(guid, email,url);

            return true;
        }

        public async Task<bool> isEmailExistAsync(string email)
        {
            return await _userRepository.IsEmailExistAsync(email);
        }


        private string HashPassword(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            var md5 = MD5.Create();
            var hashData = md5.ComputeHash(data);
            var hashedPassword = BitConverter.ToString(hashData);
            return hashedPassword;
        }


    }
}
