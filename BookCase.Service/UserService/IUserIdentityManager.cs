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
        private IUserRepository Repository { get; set; }
        private SignInManager<Domain.User.User> SignInManager { get; set; }

        public UserIdentityManager(IUserRepository userRepository, SignInManager<Domain.User.User> signInManager)
        {
            this.Repository = userRepository;
            this.SignInManager = signInManager;
        }

        public async Task<SignInResult> Login(string email, string password)
        {
            var user = await this.Repository.GetUserByEmailPassword(email, password);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            await SignInManager.SignInAsync(user, false);

            return SignInResult.Success;

        }

        public async Task Logout()
        {
            await this.SignInManager.SignOutAsync();
        }

    }
}
