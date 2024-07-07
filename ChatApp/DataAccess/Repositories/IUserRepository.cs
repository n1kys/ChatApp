using ChatApp.DataAccess.Data;

namespace ChatApp.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
    }
}
