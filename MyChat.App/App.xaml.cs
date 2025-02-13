
namespace MyChat.App
{
    using System.Windows;
    using MyChat.App.Service;
    using MyChat.App.Store;
    using MyChat.App.ViewModel;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore navigationStore;
        private readonly NavigationService navigationService;
        private Session session;

        public App()
        {
            this.navigationStore = new NavigationStore();
            this.navigationService = new NavigationService(this.navigationStore);
            this.session = new Session();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            this.navigationStore.CurrentViewModel = new UserListViewModel(this.navigationService);

            this.MainWindow = new MainView()
            {
                DataContext = new MainViewModel(this.session, this.navigationStore)
            };

            this.MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
