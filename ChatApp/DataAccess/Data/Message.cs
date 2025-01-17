﻿namespace ChatApp.DataAccess.Data
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}
