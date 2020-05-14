using System.Threading.Tasks;

namespace OneNote.Sample.Api.Helpers
{
    using Microsoft.Graph;
    using System.Net.Http;
    using System.Threading;

    /// <summary>
    /// This class allows an implementation of IAuthenticationProvider to be inserted into the DelegatingHandler
    /// pipeline of an HttpClient instance. In future versions of GraphSDK, many cross-cutting concerns will
    /// be implemented as DelegatingHandlers. This AuthHandler will come in the box.
    /// </summary>
    public class AuthHandler : DelegatingHandler
    {
        private readonly IAuthenticationProvider authenticationProvider;

        public AuthHandler(IAuthenticationProvider authenticationProvider, HttpMessageHandler innerHandler)
        {
            InnerHandler = innerHandler;
            this.authenticationProvider = authenticationProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await authenticationProvider.AuthenticateRequestAsync(request);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
