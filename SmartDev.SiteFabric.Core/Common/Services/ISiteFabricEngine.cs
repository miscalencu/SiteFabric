using SmartDev.SiteFabric.Core.Common.Models;
using System.Collections.Generic;

namespace SmartDev.SiteFabric.Core.Common.Services
{
    public interface ISiteFabricEngine
    {
        public IList<SiteFabricModule> Modules { get; set; }


    }
}
