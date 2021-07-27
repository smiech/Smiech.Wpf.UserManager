using Prism.Regions;
using Smiech.Wpf.UserManager.Core.Mvvm;
using Smiech.Wpf.UserManager.Services.Interfaces;

namespace Smiech.Wpf.UserManager.Modules.Main.ViewModels
{
    public class ViewAViewModel : RegionViewModelBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewAViewModel(IRegionManager regionManager, IMessageService messageService) :
            base(regionManager)
        {
            Message = messageService.GetMessage();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
