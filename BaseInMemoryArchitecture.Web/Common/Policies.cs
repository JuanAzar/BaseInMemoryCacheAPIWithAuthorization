using Microsoft.AspNetCore.Authorization;

namespace BaseInMemoryArchitecture.Web.Common
{
    public class Policies
    {
        public const string Admin = "Admin";
        public const string Client = "Client";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(Admin)
                .Build();
        }

        public static AuthorizationPolicy ClientPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(Client)
                .Build();
        }
    }
}
