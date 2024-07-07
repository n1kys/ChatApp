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
            chat.Messages.Add(message);
            await _chatRepository.SaveChangesAsync();
        }

        public async Task AddUserToChatAsync(int chatId, int userId)
        {
            var chat = await _chatRepository.GetChatByIdAsync(chatId);

            if (chat == null)
            {
                throw new ArgumentException($"Chat with ID {chatId} not found.");
            }

            chat.Users.Add(new User { Id = userId }); 

            await _chatRepository.SaveChangesAsync();
        }

    }
}
