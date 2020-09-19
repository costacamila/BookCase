using BookCase.Domain.User.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Service.UserService
{
    public class UserService : IUserService
    {
        private IUserRepository UserRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public async Task<IdentityResult> Save(BookCase.Domain.User.User user)
        {
            return await UserRepository.CreateAsync(user, default);
        }

        public async Task Update(BookCase.Domain.User.User user)
        {
            await UserRepository.UpdateUserAsync(user, default);
        }

        public BookCase.Domain.User.User FindById(Guid id)
        {
            return UserRepository.FindByIdAsync(id, default);
        }

        public async Task<Domain.User.User> GetUserByEmailPassword(string email, string password)
        {
            return await UserRepository.GetUserByEmailPassword(email, password);
        }

        public Domain.User.User GetUserByEmail(string email)
        {
            return UserRepository.GetUserByEmail(email);
        }

        public async Task<IdentityResult> Delete(Domain.User.User user)
        {
            return await UserRepository.DeleteAsync(user, default);
        }
    }
}
