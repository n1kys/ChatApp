using ChatApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using static ChatApp.DataAccess.Data.ApplicationDbContext;

namespace ChatApp.DataAccess.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chat>> GetChatsAsync()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            return await _context.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddChatAsync(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
        }

        public async Task DeleteChatAsync(Chat chat)
        {
            _context.Chats.Remove(chat);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddUserToChatAsync(int chatId, User user)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            if (chat != null)
            {
                chat.Users.Add(user);
                await SaveChangesAsync();
            }
        }
    }
}
