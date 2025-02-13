namespace MyChat.Server.Repository
{
    using MyChat.App.DataAccess;
    using MyChat.Core.Model;

    public class UserRepository : IUserRepository
    {
        private AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync(); // should update user-reference object
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await this.context.Users.FindAsync(id); // FindAsync searches by PK
        }
    }
}
