
using System.Threading.Tasks;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;

namespace Smiech.Wpf.UserManager.Services.Interfaces
{
    public interface IGoRestApiService
    {
        string GetMessage();
        Task<UserResponse> GetUserDataAsync(int page = 1);
        Task CreateUser(User userToCreate);
        Task UpdateUser(User userToUpdate);
        Task DeleteUser(int userId);
    }
}
