using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Regions;
using Smiech.Wpf.UserManager.Core.Mvvm;
using Smiech.Wpf.UserManager.Services.Interfaces;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;

namespace Smiech.Wpf.UserManager.Modules.Main.ViewModels
{
    public class UserManagerViewModel : RegionViewModelBase
    {
        private readonly IGoRestApiService _goRestApiService;
        private string _message;
        private ObservableCollection<UserViewModel> _userViewModels;
        private bool _isBusy;
        private Pagination _pagination;
        private const int DefaultPageNumber = 1;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public Pagination Pagination
        {
            get => _pagination;
            set => SetProperty(ref _pagination, value);
        }

        public ObservableCollection<UserViewModel> UserViewModels
        {
            get => _userViewModels;
            set => SetProperty(ref _userViewModels, value);
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public UserManagerViewModel(IRegionManager regionManager, IGoRestApiService goRestApiService) :
            base(regionManager)
        {
            _goRestApiService = goRestApiService;
            Message = goRestApiService.GetMessage();
            LoadData(DefaultPageNumber);
        }

        private async Task LoadData(int pageNumber)
        {
            IsBusy = true;
            var userResponse = await _goRestApiService.GetUserDataAsync();
            var userViewModels = userResponse.Data.Select(x => new UserViewModel(x));
            UserViewModels = new ObservableCollection<UserViewModel>(userViewModels);
            IsBusy = false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
