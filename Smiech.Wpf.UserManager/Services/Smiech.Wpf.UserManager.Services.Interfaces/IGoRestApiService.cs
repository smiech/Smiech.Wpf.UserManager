
using System.Threading.Tasks;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;

namespace Smiech.Wpf.UserManager.Services.Interfaces
{
    public interface IGoRestApiService
    {
        Task<UserResponse> GetUserDataAsync(int page = 1);
        Task<UserResponse> GetUserDataByQuery(UserQuery query, int page = 1);
        Task<SingleUserResponse> CreateUser(User userToCreate);
        Task UpdateUser(User userToUpdate);
        Task DeleteUser(int userId);
    }
}
