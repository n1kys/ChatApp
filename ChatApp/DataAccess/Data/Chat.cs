using System.Text.Json.Serialization;


namespace ChatApp.DataAccess.Data
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public List<User> Users { get; set; } = new List<User>();

        [JsonIgnore]
        public List<Message> Messages { get; set; }
    }

}
