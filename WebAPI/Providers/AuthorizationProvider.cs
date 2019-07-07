using Detention_facility.Business;
using Detention_facility.Models;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Detention_facility.Controllers
{
    public class AuthorizationProvider : OAuthAuthorizationServerProvider
    {
        private IAuthorizationService _authorizationService;

        public AuthorizationProvider(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("http://localhost:4200", new[] { "*" });
            User user = _authorizationService.CheckUser(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            string some = context.UserName;
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);            
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role, ClaimValueTypes.String));

            context.Validated(identity);

        }
    }
}