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


        public AuthorizeService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Registration(RegisterationModel registeration)
        {
            if ( _userRepository.IsEmailExist(registeration.Email))
                return await _userRepository.CreateAsync(new User()
                {
                    Email = registeration.Email,
                    Number = registeration.Number,
                    Role = registeration.Role,
                    Password = HashPassword(registeration.Password)
                });
            else
                return null;
        }

        public async Task<User?> LoginAsync(LoginModel login)
        {
            return await _userRepository.LoginAsync(login.Email,HashPassword(login.Password));
        }

        public async Task<bool> ConfirmEmailAsync(string email)
        {
            return await _userRepository.ConfirmEmailAsync(email);
        }

        private string HashPassword(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            var md5 = MD5.Create();
            var hashData = md5.ComputeHash(data);
            var hashedPassword = BitConverter.ToString(hashData);
            return hashedPassword;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordModel model)
        {
            return await _userRepository.ChangePassworndAsync(model.Email, model.Password, model.NewPassword);
        }

    }
}
