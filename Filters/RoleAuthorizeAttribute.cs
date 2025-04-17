using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FazendaUrbana.Filters
{
    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public RoleAuthorizeAttribute(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the user's role from the session
            var userRole = context.HttpContext.Session.GetString("UserRole");
            if (userRole == null || userRole != _role)
            {
                // If the user role does not match, redirect to the Login page
                context.Result = new RedirectToActionResult("Login", "Cliente", null);
            }
        }
    }
}