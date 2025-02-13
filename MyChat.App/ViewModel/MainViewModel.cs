namespace MyChat.App.ViewModel
{
    using MyChat.App.Commands;
    using MyChat.Core.Model;
    using MyChat.App.Store;
    using System.Windows.Input;

    internal class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore navigationStore;
        private Session session;

        public MainViewModel(Session session, NavigationStore navigationStore)
        {
            this.session = session;
            this.session.ClientConnected += OnClientConnected;

            this.navigationStore = navigationStore;
            this.navigationStore.CurrentViewModelChanged += this.OnCurrentViewModelChanged;

            this.ConnectCommand = new RelayCommand((_) => session.Connect());
        }

        private void OnClientConnected()
        {
            User user = new User()
            {
                //Username = this.session.PacketReader.ReadMessage()
            };
        }

        public ICommand ConnectCommand { get; }

        public BaseViewModel CurrentViewModel
        {
            get => this.navigationStore.CurrentViewModel;
        }

        private void OnCurrentViewModelChanged()
        {
            this.OnPropertyChanged(nameof(this.CurrentViewModel));
        }
    }
}
