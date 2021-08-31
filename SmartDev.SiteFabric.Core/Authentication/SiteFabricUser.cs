using System.Collections.Generic;
using System.Linq;

namespace SmartDev.SiteFabric.Core.Authentication
{
    public class SiteFabricUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public static class ExtensionMethods
    {
        public static IEnumerable<SiteFabricUser> WithoutPasswords(this IEnumerable<SiteFabricUser> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static SiteFabricUser WithoutPassword(this SiteFabricUser user)
        {
            user.Password = null;
            return user;
        }
    }
}
