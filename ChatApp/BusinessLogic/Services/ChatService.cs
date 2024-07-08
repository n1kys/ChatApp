using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApp.BusinessLogic.Interfaces;
using ChatApp.DataAccess.Data;
using ChatApp.DataAccess.Repositories;


namespace ChatApp.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;

        public ChatService(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Chat>> GetChatsAsync()
        {
            return await _chatRepository.GetChatsAsync();
        }

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            return await _chatRepository.GetChatByIdAsync(id);
        }

        public async Task AddChatAsync(Chat chat)
        {
            await _chatRepository.AddChatAsync(chat);
            await _chatRepository.SaveChangesAsync();
        }

        public async Task DeleteChatAsync(int id, int userId)
        {
            var chat = await _chatRepository.GetChatByIdAsync(id);
            if (chat.CreatedByUserId != userId)
            {
                throw new UnauthorizedAccessException("There are no permissions to do the operation");
            }
            await _chatRepository.DeleteChatAsync(chat);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task AddMessageAsync(Message message)
        {
            var chat = await _chatRepository.GetChatByIdAsync(message.ChatId);
            message.Timestamp = DateTime.UtcNow;
            chat.Messages.Add(message);
            await _chatRepository.SaveChangesAsync();
        }

        public async Task AddUserToChatAsync(int chatId, int userId)
        {
            var chat = await _chatRepository.GetChatByIdAsync(chatId);
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (chat != null && user != null)
            {
                chat.Users.Add(user);
                await _chatRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Chat or User not found");
            }
        }

    }
}
