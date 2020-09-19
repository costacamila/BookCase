using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Service.UserService
{
    public interface IUserService
    {
        Task<IdentityResult> Save(BookCase.Domain.User.User user);
        BookCase.Domain.User.User FindById(Guid id);
        Task Update(BookCase.Domain.User.User user);
        Task<IdentityResult> Delete(BookCase.Domain.User.User user);
        Domain.User.User GetUserByEmail(string email);
        Task<BookCase.Domain.User.User> GetUserByEmailPassword(string email, string password);
    }
}
