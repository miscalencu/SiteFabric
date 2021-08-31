using Microsoft.AspNetCore.Builder;

namespace SmartDev.SiteFabric.Common
{
    public static class SiteFabricAdminRoutingMiddleware
    {
        public static void UserSiteFabricAdminRouting(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;


                // Rewrite to Fabric Admin
                if (url.Contains("/admin"))
                {
                    // rewrite and continue processing
                    context.Request.Path = "/Fabric";
                }

                // Rewrite to Fabric Web

                await next();
            });
        }
    }
}
