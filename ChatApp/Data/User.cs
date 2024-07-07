namespace ChatApp.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<Chat> CreatedChats { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
