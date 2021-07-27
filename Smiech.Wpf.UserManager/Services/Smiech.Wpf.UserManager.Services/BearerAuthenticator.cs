using RestSharp;
using RestSharp.Authenticators;

namespace Smiech.Wpf.UserManager.Services
{
    public class BearerAuthenticator : IAuthenticator
    {
        private readonly string _authToken;

        public BearerAuthenticator(string authToken)
        {
            _authToken = authToken;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("authorization", "Bearer " + _authToken);
        }
    }
}
