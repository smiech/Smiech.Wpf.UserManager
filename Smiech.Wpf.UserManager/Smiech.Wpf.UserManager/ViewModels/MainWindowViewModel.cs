using Prism.Mvvm;

namespace Smiech.Wpf.UserManager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "User Manager Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
