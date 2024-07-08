using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using ChatApp.BusinessLogic.Services;
using ChatApp.DataAccess.Repositories;
using ChatApp.DataAccess.Data;

namespace ChatApp.Tests
{
    public class ChatServiceIntegrationTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ChatServiceIntegrationTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        public void Dispose()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task AddMessageAsync_ValidMessage_AddsMessageToDatabase()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var initialChat = new Chat { Id = 1, Name = "Test Chat" };
                await dbContext.Chats.AddAsync(initialChat);
                await dbContext.SaveChangesAsync();

                var chatService = new ChatService(new ChatRepository(dbContext), new UserRepository(dbContext));

                var message = new Message
                {
                    ChatId = 1,
                    UserId = 1,
                    Text = "Test message",
                    Timestamp = DateTime.Now
                };

                // Act
                await chatService.AddMessageAsync(message);

                // Assert
                var addedMessage = await dbContext.Messages.FirstOrDefaultAsync();
                Assert.NotNull(addedMessage);
                Assert.Equal("Test message", addedMessage.Text);
            }
        }
    }
}
