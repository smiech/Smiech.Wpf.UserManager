using System.Collections;
using System.Collections.Generic;
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
        private IList<User> _users;

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
            LoadData();
        }

        private async Task LoadData()
        {
            var userResponse = await _goRestApiService.GetUserDataAsync();
            Users = userResponse.Data.ToList();

        }

        public IList<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
