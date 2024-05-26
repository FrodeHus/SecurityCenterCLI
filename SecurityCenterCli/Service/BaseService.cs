using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using SecurityCenterCli.Authentication;

namespace SecurityCenterCli.Service
{
    internal abstract class BaseService(TokenClient tokenClient, IHttpClientFactory httpClientFactory)
    {
        private readonly TokenClient _tokenClient = tokenClient;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        protected async Task<HttpClient> GetAuthenticatedClient(string[] scopes)
        {
            var tokenResult = await _tokenClient.GetAccessTokenSilently(scopes);
            if (tokenResult.IsFailure)
            {
                throw new InvalidOperationException(tokenResult.Error!.ToString());
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenResult.Value);
            return client;
        }
    }
}
