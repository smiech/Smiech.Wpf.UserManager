using Prism.Commands;
using Prism.Regions;
using Smiech.Wpf.UserManager.Core.Mvvm;
using Smiech.Wpf.UserManager.Services.Interfaces;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Smiech.Wpf.UserManager.Modules.Main.ViewModels
{
    public class UserManagerViewModel : RegionViewModelBase
    {
        private readonly IGoRestApiService _goRestApiService;
        private ObservableCollection<UserViewModel> _userViewModels;
        private bool _isBusy;
        private Pagination _pagination;
        private UserQueryViewModel _userQueryViewModel;
        private const int DefaultPageNumber = 1;

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { SetProperty(ref _isBusy, value); }
        }

        public Pagination Pagination
        {
            get => _pagination;
            private set => SetProperty(ref _pagination, value);
        }

        public UserQueryViewModel UserQueryViewModel
        {
            get => _userQueryViewModel;
            private set => SetProperty(ref _userQueryViewModel , value);
        }

        public ObservableCollection<UserViewModel> UserViewModels
        {
            get => _userViewModels;
            private set => SetProperty(ref _userViewModels, value);
        }

        public ICommand GoToPageCommand => new DelegateCommand<int?>(GoToPage);
        public ICommand GoToNextPageCommand => new DelegateCommand(() => GoToPage(Pagination.Page + 1));
        public ICommand GoToPreviousPageCommand => new DelegateCommand(() => GoToPage(Pagination.Page - 1));
        public ICommand CreateUserCommand => new DelegateCommand<UserViewModel>(CreateUser);
        public ICommand UpdateUserCommand => new DelegateCommand<UserViewModel>(UpdateUser);
        public ICommand DeleteUserCommand => new DelegateCommand<UserViewModel>(DeleteUser);
        public ICommand GetUsersByQueryCommand => new DelegateCommand(GetUsersByQuery);
        public ICommand ResetUserQueryCommand => new DelegateCommand(ResetUserQuery);

        public UserManagerViewModel(IRegionManager regionManager, IGoRestApiService goRestApiService) :
            base(regionManager)
        {
            _goRestApiService = goRestApiService;
            UserQueryViewModel = new UserQueryViewModel();
            LoadData(DefaultPageNumber);
        }

        private async void ResetUserQuery()
        {
            UserQueryViewModel = new UserQueryViewModel();
            await LoadData(DefaultPageNumber);
        }

        private async void GetUsersByQuery()
        {
            UserQueryViewModel.IsActive = true;
            await LoadData(DefaultPageNumber);
        }

        private async void DeleteUser(UserViewModel userViewModel)
        {
            userViewModel.IsBusy = true;
            try
            {
                await _goRestApiService.DeleteUser(userViewModel.Id);
                UserViewModels.Remove(userViewModel);
            }
            catch
            {
                DisplayError("Error deleting user");
                userViewModel.IsBusy = false;
            }
        }

        private async void UpdateUser(UserViewModel userViewModel)
        {
            userViewModel.IsBusy = true;
            var userToUpdate = DataMapper.Map(userViewModel);
            try
            {
                await _goRestApiService.UpdateUser(userToUpdate);
                userViewModel.IsDirty = false;
            }
            catch
            {
                DisplayError("Error updating user");
            }
            finally
            {
                userViewModel.IsBusy = false;
            }
        }

        private async void CreateUser(UserViewModel userViewModel)
        {
            userViewModel.IsBusy = true;
            var userToCreate = DataMapper.Map(userViewModel);
            try
            {
                var userData = await _goRestApiService.CreateUser(userToCreate);
                userViewModel.Id = userData.Data.Id;
                userViewModel.IsDirty = false;
            }
            catch
            {
                DisplayError("Error creating user");
            }
            finally
            {
                userViewModel.IsBusy = false;
            }
        }

        // poor man's error handling xD
        private void DisplayError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async Task LoadData(int pageNumber)
        {
            IsBusy = true;
            UserResponse userResponse = null;
            try
            {
                if (UserQueryViewModel.IsActive)
                {
                    userResponse =
                        await _goRestApiService.GetUserDataByQuery(DataMapper.Map(UserQueryViewModel), pageNumber);
                }
                else
                {
                    userResponse = await _goRestApiService.GetUserDataAsync(pageNumber);
                }

                var userViewModels = userResponse.Data.Select(x => new UserViewModel(x));
                UserViewModels = new ObservableCollection<UserViewModel>(userViewModels.ToList());
                Pagination = userResponse.Meta?.Pagination;
            }
            catch
            {
                DisplayError("Error loading user data");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void GoToPage(int? pageNumber)
        {
            if (pageNumber < 1 || pageNumber > Pagination.Pages)
            {
                DisplayError("Invalid page");
            }
            else
            {
                await LoadData(pageNumber ?? DefaultPageNumber);
            }
        }
    }
}
