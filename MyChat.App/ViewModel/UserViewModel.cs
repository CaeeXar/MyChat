namespace MyChat.App.ViewModel
{
    using MyChat.App.Commands;
    using MyChat.Core.Model;
    using MyChat.App.Service;
    using System.Windows.Input;

    class UserViewModel : BaseViewModel
    {
        private User user;
        private readonly NavigationService navigationService;

        public UserViewModel(NavigationService navigationService, User user)
        {
            this.navigationService = navigationService;
            this.user = user;

            this.NavigateToUserListCommand = new RelayCommand(this.NavigateToUserList);
        }

        public User User { get => this.user; }

        public string Id => this.user.Id.ToString();
        public string Email => this.user.Email;
        public string Username => this.user.Username;

        public ICommand NavigateToUserListCommand { get; }

        public override string ToString()
        {
            return this.user.ToString();
        }

        private void NavigateToUserList(object parameter)
        {
            this.navigationService.NavigateTo(new UserListViewModel(this.navigationService));
        }
    }
}
