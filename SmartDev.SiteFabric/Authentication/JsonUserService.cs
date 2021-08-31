using SmartDev.SiteFabric.Core.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDev.SiteFabric.Authentication
{
    public class JsonUserService : IJsonUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<SiteFabricUser> _users = new List<SiteFabricUser>
        {
            new SiteFabricUser { FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        public async Task<SiteFabricUser> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<SiteFabricUser>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
