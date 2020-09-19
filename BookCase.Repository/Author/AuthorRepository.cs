using BookCase.Domain.Author.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Repository.Author
{
    public class AuthorRepository : IAuthorRepository
    {
        public Task<IdentityResult> CreateAuthorAsync(Domain.Author.Author author)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAuthorAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> FindByIdAsync(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Author.Author> GetAll()
        {
            throw new NotImplementedException();
        }

        public Domain.Author.Author GetById(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAuthorAsync(Domain.Author.Author newAuthor)
        {
            throw new NotImplementedException();
        }
    }
}
