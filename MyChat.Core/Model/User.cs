namespace MyChat.Core.Model
{
    public class User
    {
        
        public User() {}

        public User(int id, string username, string email)
        {
            this.Id = id;
            this.Username = username;
            this.Email = email;
        }

        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"({this.Id}) {this.Username}";
        }
    }
}
