namespace MyChat.Server.Repository
{
    using Microsoft.EntityFrameworkCore;
    using MyChat.App.DataAccess;
    using MyChat.Core.Model;

    public class UserRepository : IUserRepository
    {
        private DbContextOptions<AppDbContext> dbContextOptions;

        public UserRepository(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task<User> AddUserAsync(User user)
        {
            using (var context = new AppDbContext(this.dbContextOptions))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync(); // should update user-reference object
                return user;
            }
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            using (var context = new AppDbContext(this.dbContextOptions))
            {
                return await context.Users.FindAsync(id); // FindAsync searches by PK
            }
        }
    }
}
