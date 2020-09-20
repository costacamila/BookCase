using BookCase.Domain.Author.Repository;
using BookCase.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Repository.Author
{
    public class AuthorRepository : IAuthorRepository
    {
        private BookCaseContext Context { get; set; }
        public AuthorRepository (BookCaseContext bookCaseContext)
        {
            this.Context = bookCaseContext;
        }
        public async Task<IdentityResult> CreateAuthorAsync(Domain.Author.Author author)
        {
            this.Context.Authors.Add(author);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAuthorAsync(Guid id)
        {
            this.Context.Authors.Remove(GetById(id));
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task<IdentityResult> FindByIdAsync(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Author.Author> GetAll()
        {
            return this.Context.Authors.Include(x => x.Books).AsEnumerable();
        }

        public Domain.Author.Author GetById(Guid authorId)
        {
            return this.Context.Authors.FirstOrDefault(x => x.Id == authorId);
        }

        public async Task<IdentityResult> UpdateAuthorAsync(Domain.Author.Author newAuthor)
        {
            var authorOld = Context.Authors.FirstOrDefault(x => x.Id == newAuthor.Id);

            authorOld.Name = newAuthor.Name;
            authorOld.Surname = newAuthor.Surname;
            authorOld.Mail = newAuthor.Mail;
            authorOld.Birthday = newAuthor.Birthday;

            Context.Authors.Update(authorOld);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;

        }
    }
}
