using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;
using Smiech.Wpf.UserManager.Services.Interfaces;
using Smiech.Wpf.UserManager.Services.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Smiech.Wpf.UserManager.Services
{
    public class GoRestApiService : IGoRestApiService
    {
        private const string UserResourcePath = "/users";
        private readonly Uri _baseUri;
        private readonly IAuthenticator _authenticator;

        public GoRestApiService(Uri baseUri, IAuthenticator authenticator)
        {
            _baseUri = baseUri;
            _authenticator = authenticator;
        }

        public Task<UserResponse> GetUserDataAsync(int page = 1)
        {
            var request = new RestRequest(UserResourcePath, Method.GET);
            request.AddQueryParameter("page", page.ToString());
            return GetClient().GetAsync<UserResponse>(request);
        }

        public async Task<UserResponse> GetUserDataByQuery(UserQuery query, int page = 1)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            if (query.Id > 0)
            {
                var response = await GetUser(query.Id.Value);
                return new UserResponse()
                {
                    Data = new List<User>() { response.Data }
                };
            }
            var request = new RestRequest(UserResourcePath, Method.GET);
            request.AddQueryParameter("page", page.ToString());
            if (!String.IsNullOrEmpty(query.Email))
            {
                request.AddQueryParameter(nameof(query.Email).ToLowerInvariant(), query.Email.ToLowerInvariant());
            }

            if (query.Gender != null)
            {
                request.AddQueryParameter(nameof(query.Gender).ToLowerInvariant(), query.Gender);
            }

            if (!String.IsNullOrEmpty(query.Name))
            {
                request.AddQueryParameter(nameof(query.Name).ToLowerInvariant(), query.Name.ToLowerInvariant());
            }

            if (query.Status != null)
            {
                request.AddQueryParameter(nameof(query.Status).ToLowerInvariant(), query.Status);
            }

            return await GetClient().GetAsync<UserResponse>(request);
        }

        public async Task<SingleUserResponse> GetUser(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            var request = new RestRequest($"{UserResourcePath}/{userId}");
            var response = await GetClient().GetAsync<SingleUserResponse>(request);
            return response;
        }

        public async Task<SingleUserResponse> CreateUser(User userToCreate)
        {
            if (userToCreate == null) throw new ArgumentNullException(nameof(userToCreate));
            var request = new RestRequest(UserResourcePath);
            request.AddJsonBody(userToCreate);
            var response = await GetClient().PostAsync<SingleUserResponse>(request);
            return response;
        }

        public async Task UpdateUser(User userToUpdate)
        {
            if (userToUpdate == null) throw new ArgumentNullException(nameof(userToUpdate));
            var request = new RestRequest($"{UserResourcePath}/{userToUpdate.Id}", Method.PUT);
            request.AddJsonBody(userToUpdate);
            await EnsureSuccessfullResponse(request);
        }

        private async Task EnsureSuccessfullResponse(RestRequest request)
        {
            var response = await GetClient().ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"UpdateUser failed with {response.StatusCode}: {response.ErrorMessage}",
                    response.ErrorException);
            }
        }

        public async Task DeleteUser(int userId)
        {
            var request = new RestRequest($"{UserResourcePath}/{userId}", Method.DELETE);
            await EnsureSuccessfullResponse(request);
        }

        private RestClient GetClient()
        {
            var restClient = new RestSharp.RestClient(_baseUri) { Authenticator = _authenticator };
            restClient.UseNewtonsoftJson(new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return restClient;
        }
    }
}
