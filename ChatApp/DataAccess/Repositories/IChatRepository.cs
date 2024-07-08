using ChatApp.DataAccess.Data;


namespace ChatApp.DataAccess.Repositories
{
    public interface IChatRepository
    {
        Task<IEnumerable<Chat>> GetChatsAsync();
        Task<Chat> GetChatByIdAsync(int id);
        Task AddChatAsync(Chat chat);
        Task AddUserToChatAsync(int chatId, User user);
        Task AddMessageAsync(Message message);
        Task DeleteChatAsync(Chat chat);
        Task SaveChangesAsync(); 
    }
}
