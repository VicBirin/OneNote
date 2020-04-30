using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OneNote.Sample.Api.Helpers
{
    /// <summary>
    /// This class encapsulates the details of getting a token from MSAL and exposes it via the 
    /// IAuthenticationProvider interface so that GraphServiceClient or AuthHandler can use it.
    /// </summary>
    /// A significantly enhanced version of this class will in the future be available from
    /// the GraphSDK team. It will support all the types of Client Application as defined by MSAL.
    internal class MsalAuthenticationProvider : IAuthenticationProvider
    {
        private readonly IPublicClientApplication clientApplication;
        private readonly string[] scopes;

        public MsalAuthenticationProvider(IPublicClientApplication clientApplication, string[] scopes)
        {
            this.clientApplication = clientApplication;
            this.scopes = scopes;
        }

        /// <summary>
        /// Update HttpRequestMessage with credentials
        /// </summary>
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var authentication = await GetAuthenticationAsync();
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(authentication.CreateAuthorizationHeader());
        }

        /// <summary>
        /// Acquire Token for user
        /// </summary>
        public async Task<AuthenticationResult> GetAuthenticationAsync()
        {
            AuthenticationResult authResult;
            var accounts = await clientApplication.GetAccountsAsync();

            try
            {
                authResult = await clientApplication.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                authResult = await clientApplication.AcquireTokenInteractive(scopes)
                        .WithAccount(accounts.FirstOrDefault())
                        .WithPrompt(Microsoft.Identity.Client.Prompt.SelectAccount)
                        .ExecuteAsync();
            }

            return authResult;
        }
    }
}
