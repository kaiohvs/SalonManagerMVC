using Microsoft.AspNetCore.Authorization;
using SalonManager.Web.Requirements;
using System.Security.Claims;

namespace SalonManager.Web.Handlers
{
    public class AdminAuthorizationHandler : AuthorizationHandler<AdminAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAuthorizationRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }       
    }
}
