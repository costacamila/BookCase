using System;
using System.Threading;
using System.Threading.Tasks;
using BookCase.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookCase.Repository.User
{
    public class RoleRepository : IRoleStore<Domain.User.Role>
    {
        private bool disposedValue;

        private BookCaseContext Context { get; set; }

        public RoleRepository(BookCaseContext aTDsvAspNetContext)
        {
            this.Context = aTDsvAspNetContext;
        }

        public async Task<IdentityResult> CreateAsync(Domain.User.Role role, CancellationToken cancellationToken)
        {
            this.Context.Profiles.Add(role);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Domain.User.Role role, CancellationToken cancellationToken)
        {
            this.Context.Profiles.Remove(role);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<Domain.User.Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await this.Context.Profiles.FirstOrDefaultAsync(x => x.Id == new Guid(roleId));
        }

        public async Task<Domain.User.Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await this.Context.Profiles.FirstOrDefaultAsync(x => x.Name == normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(Domain.User.Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task<string> GetRoleIdAsync(Domain.User.Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Domain.User.Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Domain.User.Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Name = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Domain.User.Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Domain.User.Role role, CancellationToken cancellationToken)
        {
            var roleToUpdate = await this.Context.Profiles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == role.Id);

            roleToUpdate = role;
            this.Context.Entry(roleToUpdate).State = EntityState.Modified;

            this.Context.Profiles.Add(roleToUpdate);
            await this.Context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ProfileRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}
