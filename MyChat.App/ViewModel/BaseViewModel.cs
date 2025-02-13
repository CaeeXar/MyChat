namespace MyChat.App.ViewModel
{
    using System.ComponentModel;

    internal class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string properyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }
    }
}
