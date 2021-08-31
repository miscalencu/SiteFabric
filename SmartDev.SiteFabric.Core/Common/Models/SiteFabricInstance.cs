using Microsoft.Extensions.DependencyInjection;

namespace SmartDev.SiteFabric.Core.Common.Models
{
    public class SiteFabricInstance
    {
        public SiteFabricInstance(IServiceCollection _services)
        {
            Services = _services;
        }

        public IServiceCollection Services;
    }

    public class AuthenticatedSiteFabricInstance
    {
        public AuthenticatedSiteFabricInstance(SiteFabricInstance _instance)
        {
            Instance = _instance;
        }

        public SiteFabricInstance Instance;
    }
}
