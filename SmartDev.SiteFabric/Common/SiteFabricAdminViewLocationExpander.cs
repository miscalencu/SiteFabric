using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartDev.SiteFabric.Common
{
    public class SiteFabricAdminViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var assembly = typeof(Startup).GetTypeInfo().Assembly;
            var provider = new CompositeFileProvider(new EmbeddedFileProvider(assembly));
            var contents = provider.GetDirectoryContents(string.Empty);
            var assemblyName = assembly.GetName().Name;

            viewLocations = viewLocations.Concat(contents.Select(x => x.Name.ToString()));

            viewLocations = viewLocations.Concat(Assembly.GetExecutingAssembly().GetManifestResourceNames().Select(x => "/" + x));

            viewLocations = viewLocations.Concat(new[]
            {
                $"~/Views/{{1}}/{{0}}.cshtml",
                $"/Views.{{1}}.{{0}}.cshtml",
                $"{assemblyName}.Views.{{1}}.{{0}}.cshtml",
                $"/{assemblyName}.Views.{{1}}.{{0}}.cshtml",
                $"/{assemblyName}/Views.{{1}}.{{0}}.cshtml",

                $"/{assemblyName}/Views/{{1}}/{{0}}.cshtml",    
                $"/{assemblyName}/Views/Shared/{{0}}.cshtml"
            });

            // foreach (var viewLocation in viewLocations)
            //    viewLocations = viewLocations.Concat("/" + assemblyName + viewLocation);

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            
        }
    }
}
