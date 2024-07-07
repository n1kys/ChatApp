using ChatApp.BusinessLogic.Interfaces;
using ChatApp.DataAccess.Data;
using ChatApp.DataAccess.Repositories;
using static ChatApp.DataAccess.Data.ApplicationDbContext;

namespace ChatApp.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }
    }
}
