using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SmartDev.SiteFabric.Authentication;
using SmartDev.SiteFabric.Common;
using SmartDev.SiteFabric.Core.Authentication;
using SmartDev.SiteFabric.Core.Common.Models;
using SmartDev.SiteFabric.Core.Common.Services;
using System;
using System.Reflection;

namespace SmartDev.SiteFabric
{
    public static class Startup
    {
        public static SiteFabricInstance AddSiteFabric(this IServiceCollection services)
        {
            services.AddSingleton<ISiteFabricEngine, SiteFabricEngine>();

            var siteFabricAssembly = typeof(Razor.Areas.Fabric.Controllers.HomeController).Assembly;
            var part = new AssemblyPart(siteFabricAssembly);

            services.AddControllersWithViews()
                .AddApplicationPart(siteFabricAssembly)
                .AddRazorRuntimeCompilation();

            services.Configure<MvcRazorRuntimeCompilationOptions>(options => { options.FileProviders.Add(new EmbeddedFileProvider(siteFabricAssembly)); });

            /*
            services.AddControllersWithViews()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new SiteFabricAdminViewLocationExpander());
            });

            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {   
                options.FileProviders.Clear();
                options.FileProviders.Add(new PhysicalFileProvider(AppContext.BaseDirectory));
                options.FileProviders.Add(new CompositeFileProvider(new EmbeddedFileProvider(siteFabricAssembly, "SiteFabric")));
                //new PhysicalFileProvider(appDirectory));
            });
            */

            return new SiteFabricInstance(services);
        }

        public static AuthenticatedSiteFabricInstance AddJsonAuthentication(this SiteFabricInstance instance)
        {
            // Identity Services
            instance.Services.AddIdentity<SiteFabricUser, SiteFabricRole>()
                .AddUserStore<JsonUserStore<SiteFabricUser>>()
                .AddRoleStore<JsonRoleStore<SiteFabricRole>>();
            
            return new AuthenticatedSiteFabricInstance(instance);
        }

        public static void UseSiteFabric(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UserSiteFabricAdminRouting();

            app.UseRouting();

            app.UserSiteFabricBasicAuth();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "Fabric",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
