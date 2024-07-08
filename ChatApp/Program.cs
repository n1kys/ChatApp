using ChatApp.BusinessLogic.Interfaces;
using ChatApp.BusinessLogic.Services;
using ChatApp.DataAccess.Data;
using ChatApp.DataAccess.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static ChatApp.DataAccess.Data.ApplicationDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
           });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps(); 
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/chats", async (Chat chat, IChatService chatService) =>
{
    await chatService.AddChatAsync(chat);
    return Results.Created($"/api/chats/{chat.Id}", chat);
});

app.MapGet("/api/chats", async (IChatService chatService) =>
{
    return Results.Ok(await chatService.GetChatsAsync());
});

app.MapGet("/api/chats/{id}", async (int id, IChatService chatService) =>
{
    var chat = await chatService.GetChatByIdAsync(id);
    return chat is not null ? Results.Ok(chat) : Results.NotFound();
});

app.MapDelete("/api/chats/{id}", async (int id, int userId, IChatService chatService) =>
{
    try
    {
        await chatService.DeleteChatAsync(id, userId);
        return Results.NoContent();
    }
    catch (UnauthorizedAccessException ex)
    {
        return Results.Forbid();
    }
});

app.MapPost("/api/users", async (User user, IUserService userService) =>
{
    await userService.AddUserAsync(user);
    return Results.Created($"/api/users/{user.Id}", user);
});

app.MapPost("/api/chats/{chatId}/users/{userId}", async (int chatId, int userId, IChatService chatService) =>
{
    try
    {
        await chatService.AddUserToChatAsync(chatId, userId);
        return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("/api/chats/{chatId}/messages", async (int chatId, Message message, IChatService chatService) =>
{
    try
    {
        message.ChatId = chatId;
        await chatService.AddMessageAsync(message);
        return Results.Created($"/api/chats/{chatId}/messages/{message.Id}", message);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/api/users/{id}", async (int id, IUserService userService) =>
{
    var user = await userService.GetUserByIdAsync(id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.Run();
