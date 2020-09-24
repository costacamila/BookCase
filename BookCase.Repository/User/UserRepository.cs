using BookCase.Domain.User.Repository;
using BookCase.Repository.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookCase.Repository.User
{
    public class UserRepository : IUserStore<Domain.User.User>, IUserRepository
    {
        private bool disposedValue;

        private BookCaseContext Context { get; set; }

        public UserRepository(BookCaseContext bookCaseContext)
        {
            this.Context = bookCaseContext;
        }
        public async Task<IdentityResult> CreateAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            try
            {
                this.Context.Users.Add(user);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            this.Context.Users.Remove(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Domain.User.User FindByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return this.Context.Users.FirstOrDefault(x => x.Id == userId);
        }

        public Task<Domain.User.User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.User.User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Mail);
        }

        public Domain.User.User GetUserByEmail(string email)
        {
            return this.Context.Users.FirstOrDefault(x => x.Mail == email);
        }
        public Domain.User.User GetUserById(Guid id)
        {
            return this.Context.Users.FirstOrDefault(x => x.Id == id);
        }

        public Task<Domain.User.User> GetUserByEmailPassword(string email, string password)
        {
            return Task.FromResult(this.Context.Users.FirstOrDefault(x => x.Mail == email && x.Password == password));
        }

        public Domain.User.User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Mail.ToString());
        }

        public Task SetNormalizedUserNameAsync(Domain.User.User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Mail = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Domain.User.User user, string userName, CancellationToken cancellationToken)
        {
            user.Mail = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateUserAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #region Dispose Implementation

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
        // ~AccountRepository()
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

        //public Task<Domain.User.User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

    }
}
