using BookCase.Domain.Author.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
            return await this.AuthorRepository.UpdateAuthorAsync(newAuthor);
        }

        public async Task<IdentityResult> DeleteAuthorAsync(Guid id)
        {
            return await this.AuthorRepository.DeleteAuthorAsync(id);
        }
    }
}
