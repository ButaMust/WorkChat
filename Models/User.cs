namespace WorkChat.Models
{
    public class User
    {
        public int Id { get; set; }
        public required String Name { get; set; }
        public required String LastName { get; set; }
        public required String Email { get; set; }
        public required String Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        }
    }
