namespace MyChat.Server.Repository
{
    using MyChat.Core.Model;

    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User user);   
    }
}
