using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace SalonManager.Web.Filters
{
    public class NonAdminUserControllerAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var isAdmin = context.HttpContext.User.IsInRole("Admin");

            if (!isAdmin && await IsUserController(context))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
        private async Task<bool> IsUserController(AuthorizationFilterContext context)
        {
            var controllerType = context.ActionDescriptor.EndpointMetadata
           .OfType<ControllerActionDescriptor>()
           .FirstOrDefault()?.ControllerTypeInfo;

            return controllerType != null && controllerType.Name == "UserController";
        }
    }
}
