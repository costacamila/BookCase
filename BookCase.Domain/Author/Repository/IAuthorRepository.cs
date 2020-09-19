using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Domain.Author.Repository
{
    public interface IAuthorRepository
    {
        Task<IdentityResult> CreateAuthorAsync(Domain.Author.Author author);
        Task<IdentityResult> UpdateAuthorAsync(Domain.Author.Author newAuthor);
        Task<IdentityResult> FindByIdAsync(Guid authorId);
        Task<IdentityResult> DeleteAuthorAsync(Guid id);

        IEnumerable<Domain.Author.Author> GetAll();
        Domain.Author.Author GetById(Guid authorId);
    }
}
