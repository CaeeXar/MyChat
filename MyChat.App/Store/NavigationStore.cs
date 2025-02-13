namespace MyChat.App.Store
{
    using MyChat.App.ViewModel;

    internal class NavigationStore
    {
        private BaseViewModel currentViewModel = null!;

        public BaseViewModel CurrentViewModel
        {
            get => this.currentViewModel;
            set
            {
                this.currentViewModel = value;
                this.OnCurrentViewModelChanged();
            }
        }

        public event Action? CurrentViewModelChanged;

        protected void OnCurrentViewModelChanged()
        {
            this.CurrentViewModelChanged?.Invoke();
        }

    }
}
