namespace MyChat.App.ViewModel
{
    using MyChat.App.Commands;
    using MyChat.Core.Model;
    using MyChat.App.Service;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    internal class UserListViewModel : BaseViewModel
    {
        private readonly NavigationService navigationService;
        private string searchText = string.Empty;
        private readonly ObservableCollection<UserViewModel> users;
        private ObservableCollection<UserViewModel> filteredUsers;

        public UserListViewModel(NavigationService navigationService)
        {
            // service
            this.navigationService = navigationService;

            // user
            this.users =
            [
                new UserViewModel(navigationService, new User(1, "Samuel", "s@.at")),
                new UserViewModel(navigationService, new User(2, "Alfred", "a@.at")),
                new UserViewModel(navigationService, new User(3, "Dave", "d@.de")),
            ];
            this.filteredUsers = new ObservableCollection<UserViewModel>(this.users);

            // command
            this.NavigateToUserCommand = new RelayCommand((user) => this.NavigateToUser(user));
        }

        public ICommand NavigateToUserCommand { get; }

        public IEnumerable<UserViewModel> Users => this.users;

        public ObservableCollection<UserViewModel> FilteredUsers
        {
            get => this.filteredUsers;
            set
            {
                this.filteredUsers = value;
                this.OnPropertyChanged(nameof(this.FilteredUsers));
            }
        }

        public string SearchText
        {
            get
            {
                return this.searchText;
            }
            set
            {
                this.searchText = value;
                this.OnPropertyChanged(nameof(this.SearchText));
                this.Search();
            }
        }

        private void Search()
        {
            string text = this.SearchText.Trim();
            if (text.Length < 3)
            {
                this.FilteredUsers.Clear();
                foreach (var u in this.Users) this.FilteredUsers.Add(u);
            }
            else
            {
                var filtered = this.Users
                    .Where(u =>
                        u.Username.Contains(text, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                this.FilteredUsers.Clear();
                foreach (var u in filtered) this.FilteredUsers.Add(u);
            }
        }
        
        private void NavigateToUser(object user)
        {
            if (user is not UserViewModel)
            {
                throw new Exception($"{nameof(user)} is not an instance of {nameof(UserViewModel)}");
            }

            this.navigationService.NavigateTo((UserViewModel)user);
        }
    }
}
