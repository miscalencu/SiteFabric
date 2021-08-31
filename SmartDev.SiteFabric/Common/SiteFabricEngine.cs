using SmartDev.SiteFabric.Core.Common.Models;
using SmartDev.SiteFabric.Core.Common.Services;
using System.Collections.Generic;

namespace SmartDev.SiteFabric.Common
{
    public class SiteFabricEngine : ISiteFabricEngine
    {
        public IList<SiteFabricModule> Modules { get; set; }
    }
}
