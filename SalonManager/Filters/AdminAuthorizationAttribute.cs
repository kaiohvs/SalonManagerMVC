using Microsoft.AspNetCore.Authorization;

namespace SalonManager.Web.Filters
{
    public class AdminAuthorizationAttribute : AuthorizeAttribute
    {
        public AdminAuthorizationAttribute() :base("AdminPolicy") { }
    }
}
