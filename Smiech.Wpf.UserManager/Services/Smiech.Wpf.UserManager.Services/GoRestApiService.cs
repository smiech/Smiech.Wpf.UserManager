using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Smiech.Wpf.UserManager.Services.Interfaces;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;

namespace Smiech.Wpf.UserManager.Services
{
    public class GoRestApiService : IGoRestApiService
    {
        private readonly Uri _baseUri;
        private readonly IAuthenticator _authenticator;

        public GoRestApiService(Uri baseUri, IAuthenticator authenticator)
        {
            _baseUri = baseUri;
            _authenticator = authenticator;
        }

        public string GetMessage()
        {
            return "Hello from the Message Service";
        }

        public Task<UserResponse> GetUserDataAsync(int page = 1)
        {
            var request = new RestRequest("/users", Method.GET);
            request.AddQueryParameter("page", page.ToString());
            var response =  GetClient().GetAsync<UserResponse>(request);
            return response;
        }

        public Task CreateUser(User userToCreate)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUser(User userToUpdate)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        private RestClient GetClient()
        {
            var restClient = new RestSharp.RestClient(_baseUri) { Authenticator = _authenticator };
            return restClient;
        }
    }
}
