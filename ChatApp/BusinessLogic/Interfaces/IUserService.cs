using ChatApp.DataAccess.Data;

namespace ChatApp.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
    }
}
