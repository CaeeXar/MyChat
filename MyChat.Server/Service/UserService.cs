namespace MyChat.Server.Service
{
    using MyChat.Core.Model;
    using MyChat.Server.Repository;

    // TODO: Password hashing also here (bcrypt)

    public class UserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await this.userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new Exception($"User with ID \"{id}\" was not found.");
            }

            return user;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            // validation here...
            if (string.IsNullOrEmpty(user.Email.Trim()))
            {
                throw new Exception("User e-mail wrong!");
            }
            
            // validated user, continue
            return await this.userRepository.AddUserAsync(user);
        }
    }
}
