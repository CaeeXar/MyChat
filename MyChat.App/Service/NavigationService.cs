namespace MyChat.App.Service
{
    using MyChat.App.Store;
    using MyChat.App.ViewModel;

    internal class NavigationService
    {
        private readonly NavigationStore navigationStore;

        public NavigationService(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
        }

        public void NavigateTo(Func<BaseViewModel> createViewModel)
        {
            this.navigationStore.CurrentViewModel = createViewModel();
        }

        public void NavigateTo(BaseViewModel viewModel)
        {
            this.navigationStore.CurrentViewModel = viewModel;
        }
    }
}
