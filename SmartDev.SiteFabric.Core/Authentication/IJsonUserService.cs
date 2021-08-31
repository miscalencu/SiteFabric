using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDev.SiteFabric.Core.Authentication
{
    public interface IJsonUserService
    {
        Task<SiteFabricUser> Authenticate(string username, string password);
        Task<IEnumerable<SiteFabricUser>> GetAll();
    }
}
