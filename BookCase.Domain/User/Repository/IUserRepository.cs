using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookCase.Domain.User.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(Domain.User.User user, CancellationToken cancellationToken);

        Domain.User.User FindByIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<IdentityResult> UpdateUserAsync(Domain.User.User user, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync(Domain.User.User user, CancellationToken cancellationToken);

        Domain.User.User GetUserByEmail(string email);

        Domain.User.User GetUserByUsername(string username);

        Task<User> GetUserByEmailPassword(string email, string password);
    }
}