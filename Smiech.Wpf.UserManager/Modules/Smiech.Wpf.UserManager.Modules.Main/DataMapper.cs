using Smiech.Wpf.UserManager.Modules.Main.ViewModels;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;

namespace Smiech.Wpf.UserManager.Modules.Main
{
    static class DataMapper
    {
        public static User ToUserModel(UserViewModel userViewModel)
        {
            return new User()
            {
                Email = userViewModel.Email,
                Gender = userViewModel.Gender,
                Id = userViewModel.Id,
                Name = userViewModel.Name,
                Status = userViewModel.Status
            };
        }
    }
}
