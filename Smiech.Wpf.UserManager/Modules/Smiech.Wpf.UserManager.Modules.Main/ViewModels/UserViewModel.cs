using System;
using System.ComponentModel;
using Smiech.Wpf.UserManager.Core.Mvvm;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;

namespace Smiech.Wpf.UserManager.Modules.Main.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private int _id;
        private string _name;
        private string _email;
        private string _gender;
        private string _status;
        private bool _isDirty;
        private bool _isBusy;

        public UserViewModel()
        {
        }

        public UserViewModel(User userModel)
        {
            if (userModel == null) throw new ArgumentNullException(nameof(userModel));

            _name = userModel.Name;
            _email = userModel.Email;
            _gender = userModel.Gender;
            _id = userModel.Id;
            _status = userModel.Status;
        }

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                // dirty hack, heh
                if (Id != 0)
                {
                    SetProperty(ref _isDirty, value);
                }
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(IsBusy) && args.PropertyName != nameof(IsDirty))
            {
                IsDirty = true;
            }

            base.OnPropertyChanged(args);
        }
    }
}
