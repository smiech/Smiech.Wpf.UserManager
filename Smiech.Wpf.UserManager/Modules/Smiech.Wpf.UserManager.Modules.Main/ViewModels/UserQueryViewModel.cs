using System.ComponentModel;
using Smiech.Wpf.UserManager.Core.Mvvm;

namespace Smiech.Wpf.UserManager.Modules.Main.ViewModels
{
    public class UserQueryViewModel : ViewModelBase
    {
        private bool _isActive;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }
    }
}
