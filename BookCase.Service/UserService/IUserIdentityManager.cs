using BookCase.Domain.User.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Service.UserService
{
    public interface IUserIdentityManager
    {
        Task<SignInResult> Login(string userName, string password);
        Task Logout();
    }
}
