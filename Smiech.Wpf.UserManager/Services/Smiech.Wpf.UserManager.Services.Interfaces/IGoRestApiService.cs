
using Smiech.Wpf.UserManager.Services.Interfaces.Models;
using System.Threading.Tasks;

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
