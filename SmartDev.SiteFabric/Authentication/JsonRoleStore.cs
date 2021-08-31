using Microsoft.AspNetCore.Identity;
using SmartDev.SiteFabric.Core.Authentication;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartDev.SiteFabric.Authentication
{
    public class JsonRoleStore<TRole> : IRoleStore<TRole> where TRole : SiteFabricRole
    {
        public Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
