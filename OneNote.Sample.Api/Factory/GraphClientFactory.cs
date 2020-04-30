using Microsoft.Graph;
using System.Collections.Generic;
using System.Linq;

namespace OneNote.Sample.Api
{
    using Helpers;

    using Microsoft.Identity.Client;
    using System.Configuration;
    using System.Globalization;

    /// <summary>
    /// This static class returns a fully constructed 
    /// instance of the GraphServiceClient with the client 
    /// data to be used when authenticating requests to the Graph API
    /// </summary> 
    internal static class GraphClientFactory
    {
        private static readonly string clientId;
        private static readonly string instance;
        private static readonly string[] scopes;
        private static readonly string tenantId;
        private static string authority;

        static GraphClientFactory()
        {
            clientId = ConfigurationManager.AppSettings["ClientId"];
            instance = ConfigurationManager.AppSettings["Instance"];
            scopes = ConfigurationManager.AppSettings["Scopes"].Split(',');
            tenantId = ConfigurationManager.AppSettings["TenantId"];
            authority = string.Format(CultureInfo.InvariantCulture, instance, tenantId);
        }

        public static GraphServiceClient GetGraphServiceClient()
        {
            var authenticationProvider = CreateAuthorizationProvider();
            return new GraphServiceClient(authenticationProvider);
        }

        private static IAuthenticationProvider CreateAuthorizationProvider()
        {
            var clientApplication = PublicClientApplicationBuilder
                .Create(clientId)
                .WithAuthority(authority)
                .Build();

            TokenCacheHelper.EnableSerialization(clientApplication.UserTokenCache);

            return new MsalAuthenticationProvider(clientApplication, scopes.ToArray());
        }
    }
}