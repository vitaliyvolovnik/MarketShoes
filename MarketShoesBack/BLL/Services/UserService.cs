using BLL.Services.Interfaces;
using DLL.Models;
using DLL.Repositories;


namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetAsync(int id)
        {
            return  await _userRepository.FindFirstAsync(x=>x.Id == id);
        }
    }
}
