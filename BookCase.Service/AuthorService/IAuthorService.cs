using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Service.AuthorService
{
    public interface IAuthorService
    {
        Task<IdentityResult> SaveAuthor(BookCase.Domain.Author.Author author);

        IEnumerable<Domain.Author.Author> GetAll();
        Domain.Author.Author GetById(Guid authorId);

        Task<IdentityResult> UpdateAuthorAsync(Domain.Author.Author newAuthor);
        Task<IdentityResult> DeleteAuthorAsync(Guid id);
    }
}
