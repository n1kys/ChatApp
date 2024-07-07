namespace ChatApp.Data
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
