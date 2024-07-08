using System.Threading.Tasks;
using Xunit;
using Moq;
using ChatApp.BusinessLogic.Services;
using ChatApp.DataAccess.Repositories;
using ChatApp.BusinessLogic.Interfaces;
using ChatApp.DataAccess.Data;

namespace ChatApp.Tests
{
    public class ChatServiceTests
    {
        [Fact]
        public async Task GetChatByIdAsync_ValidId_ReturnsChat()
        {
            // Arrange
            var mockChatRepo = new Mock<IChatRepository>();
            var mockUserRepo = new Mock<IUserRepository>();

            var chatId = 1;
            var expectedChat = new Chat { Id = chatId, Name = "Test Chat" };

            mockChatRepo.Setup(repo => repo.GetChatByIdAsync(chatId))
                        .ReturnsAsync(expectedChat); // Setup mock for GetChatByIdAsync

            var chatService = new ChatService(mockChatRepo.Object, mockUserRepo.Object);

            // Act
            var result = await chatService.GetChatByIdAsync(chatId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedChat.Id, result.Id);
            Assert.Equal(expectedChat.Name, result.Name);
        }
    }
}
