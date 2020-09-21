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
        private BookCaseContext Context { get; set; }

        public UserRepository(BookCaseContext bookCaseContext)
        {
            this.Context = bookCaseContext;
        }
        public async Task<IdentityResult> CreateAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            this.Context.Users.Add(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            this.Context.Users.Remove(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.User.User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Domain.User.User FindByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.User.User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        public Task<Domain.User.User> GetUserByUserNamePassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(Domain.User.User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(Domain.User.User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateUserAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
