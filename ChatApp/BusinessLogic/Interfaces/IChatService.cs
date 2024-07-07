using ChatApp.DataAccess.Data;

namespace ChatApp.BusinessLogic.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<Chat>> GetChatsAsync();
        Task<Chat> GetChatByIdAsync(int id);
        Task AddChatAsync(Chat chat);
        Task AddUserToChatAsync(int chatId, int userId);
        Task DeleteChatAsync(int id, int userId);
        Task AddMessageAsync(Message message);
    }
}
