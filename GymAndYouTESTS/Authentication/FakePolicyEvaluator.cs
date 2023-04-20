using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;

namespace GymAndYouTESTS.Authentication
{
    public class FakePolicyEvaluator : IPolicyEvaluator
    {
        public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,TestUser.Test_User_Id.ToString()),
                    new Claim(ClaimTypes.Role,TestUser.Test_User_Role),
                }));
                   

            AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal,"test");
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
            return Task.FromResult
            (
                PolicyAuthorizationResult.Success()
            );
        }
    }
}
