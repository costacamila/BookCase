using BookCase.Domain.Author.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Service.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository AuthorRepository { get; set; }

        public AuthorService(IAuthorRepository authorRepository)
        {
            this.AuthorRepository = authorRepository;
        }

        public async Task<IdentityResult> SaveAuthor(Domain.Author.Author author)
        {
            if (this.AuthorRepository.GetAll().Where(x => (x.Name + x.Surname)
                                == (author.Name + author.Surname)).FirstOrDefault() != null
                                || this.AuthorRepository.GetAll().Where(x => x.Mail.Trim().ToLower().Replace(" ", "")
                                    == author.Mail.Trim().ToLower().Replace(" ", "")).FirstOrDefault() != null)
            {
                throw new Exception("Este autor/email já existe.");
            }
            return await AuthorRepository.CreateAuthorAsync(author);
        }

        public IEnumerable<Domain.Author.Author> GetAll()
        {
            return AuthorRepository.GetAll();
        }

        public Domain.Author.Author GetById(Guid authorId)
        {
            return AuthorRepository.GetById(authorId);
        }

        public async Task<IdentityResult> UpdateAuthorAsync(Domain.Author.Author newAuthor)
        {
            if (this.AuthorRepository.GetAll().Where(x => x.Id  == newAuthor.Id).FirstOrDefault() == null)
            {
                throw new Exception("Este autor não existe");
            }
            return await this.AuthorRepository.UpdateAuthorAsync(newAuthor);
        }

        public async Task<IdentityResult> DeleteAuthorAsync(Guid id)
        {
            if (this.AuthorRepository.GetAll().Where(x => x.Id == id).FirstOrDefault() == null)
            {
                throw new Exception("Este autor não existe");
            }
            return await this.AuthorRepository.DeleteAuthorAsync(id);
        }
    }
}
