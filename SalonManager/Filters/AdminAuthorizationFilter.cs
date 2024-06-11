using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SalonManager.Web.Requirements;

namespace SalonManager.Web.Filters
{
    public class AdminAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authorizationService;

        public AdminAuthorizationFilter(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(context.HttpContext.User, null, new AdminAuthorizationRequirement());
            if(!authorizationResult.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
