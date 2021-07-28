using Smiech.Wpf.UserManager.Modules.Main.ViewModels;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;

namespace Smiech.Wpf.UserManager.Modules.Main
{
    static class DataMapper
    {
        public static User Map(UserViewModel userViewModel)
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
        public static UserQuery Map(UserQueryViewModel userQueryViewModel)
        {
            return new UserQuery()
            {
                Email = userQueryViewModel.Email,
                Gender = userQueryViewModel.Gender,
                Id = userQueryViewModel.Id,
                Name = userQueryViewModel.Name,
                Status = userQueryViewModel.Status
            };
        }
    }
}
