using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;

using SecurityCenterCli.Common;
using SecurityCenterCli.Infrastructure;

namespace SecurityCenterCli.Authentication
{
    internal class TokenClient(Configuration configuration)
    {
        private readonly Configuration _configuration = configuration;

        public async Task<Result<string>> GetAccessTokenSilently(string[] scopes)
        {
            if (_configuration.Credential is null || _configuration.Credential.ClientId is null || _configuration.Credential.TenantId is null)
            {
                return new Error(ErrorCode.CredentialNotSet, "Please set the credentials first");
            }

            if (_configuration.Credential?.ClientSecret is null)
            {
                var cacheHelper = await MsalCacheHelper.CreateAsync(Configuration.TokenCache);
                var client = PublicClientApplicationBuilder.Create(_configuration.Credential?.ClientId)
                                               .WithAuthority(AzureCloudInstance.AzurePublic, _configuration.Credential?.TenantId)
                                               .WithRedirectUri("http://localhost")
                                               .Build();
                cacheHelper.RegisterCache(client.UserTokenCache);

                var accounts = await client.GetAccountsAsync();
                if (!accounts.Any())
                {
                    return new Error(ErrorCode.AuthenticationError, "Please login first");
                }

                var result = await client.AcquireTokenSilent(scopes, accounts.First()).ExecuteAsync();
                return result.AccessToken;
            }
            else
            {
                var client = ConfidentialClientApplicationBuilder.Create(_configuration.Credential?.ClientId)
                                               .WithClientSecret(_configuration.Credential?.ClientSecret)
                                               .WithAuthority(AzureCloudInstance.AzurePublic, _configuration.Credential?.TenantId)
                                               .WithRedirectUri("http://localhost")
                                               .Build();
                var result = await client.AcquireTokenForClient(scopes).ExecuteAsync();
                return result.AccessToken;
            }
        }

        public async Task<Result<string>> GetAccessToken()
        {
            if (_configuration.Credential is null || _configuration.Credential.ClientId is null || _configuration.Credential.TenantId is null)
            {
                return new Error(ErrorCode.CredentialNotSet, "Please set the credentials first");
            }

            var cacheHelper = await MsalCacheHelper.CreateAsync(Configuration.TokenCache);
            if (_configuration.Credential?.ClientSecret is null)
            {
                var client = PublicClientApplicationBuilder.Create(_configuration.Credential?.ClientId)
                                                           .WithAuthority(AzureCloudInstance.AzurePublic, _configuration.Credential?.TenantId)
                                                           .WithRedirectUri("http://localhost")
                                                           .Build();
                cacheHelper.RegisterCache(client.UserTokenCache);
                return (await client.AcquireTokenInteractive(new[] { "https://api.securitycenter.microsoft.com/.default" }).ExecuteAsync()).AccessToken;
            }
            else
            {
                var client = ConfidentialClientApplicationBuilder.Create(_configuration.Credential?.ClientId)
                                                                .WithClientSecret(_configuration.Credential?.ClientSecret)
                                                                .WithAuthority(AzureCloudInstance.AzurePublic, _configuration.Credential?.TenantId)
                                                                .Build();
                cacheHelper.RegisterCache(client.UserTokenCache);
                return (await client.AcquireTokenForClient(new[] { "https://api.securitycenter.microsoft.com/.default" }).ExecuteAsync()).AccessToken;
            }
        }
    }
}
