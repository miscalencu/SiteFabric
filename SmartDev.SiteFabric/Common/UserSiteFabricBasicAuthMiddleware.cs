using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SmartDev.SiteFabric.Core.Authentication;
using System;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SmartDev.SiteFabric.Common
{
    public static class UserSiteFabricBasicAuthMiddleware
    {
        public static void UserSiteFabricBasicAuth(this IApplicationBuilder app)
        {
            app.UseMiddleware<SiteFabricBasicAuthMiddleware>();
        }
    }

    public class SiteFabricBasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public SiteFabricBasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this._next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/fabric") && !context.User.Identity.IsAuthenticated)
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Get the encoded username and password
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                    // Decode from Base64 to string
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    // Split username and password
                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];

                    // Check if login is correct
                    var user = IsAuthorized(username, password);

                    if (user != null)
                    {
                        context.User = new GenericPrincipal(new GenericIdentity(username), new string[] { "Admin" });
                    }
                }

                // if still not authenticated
                if(!context.User.Identity.IsAuthenticated)
                {
                    // Return authentication type (causes browser to show login dialog)
                    context.Response.Headers["WWW-Authenticate"] = "Basic";

                    // Return unauthorized
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    // stop here if not I will be redirected to "Login" page
                    return;
                }
            }
            
            await _next.Invoke(context);
        }

        private SiteFabricUser IsAuthorized(string username, string password)
        {
            // Check that username and password are correct
            if (username.Equals(_configuration["SiteFabric:UserName"], StringComparison.InvariantCultureIgnoreCase) && password.Equals(_configuration["SiteFabric:Password"]))
            {
                return new SiteFabricUser()
                {
                    FirstName = username
                };
            }

            return null;
        }
    }
}
