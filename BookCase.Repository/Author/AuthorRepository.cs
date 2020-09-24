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
        public AuthorRepository(BookCaseContext bookCaseContext)
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
            var author = Context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id);

            using (var transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in author.Books)
                    {
                        this.Context.Books.Remove(item);
                    }
                    this.Context.Authors.Remove(author);
                    await this.Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    transaction.Rollback();
                }
            }
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
            return this.Context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == authorId);
        }

        public async Task<IdentityResult> UpdateAuthorAsync(Domain.Author.Author newAuthor)
        {
            try
            {
                var authorOld = Context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == newAuthor.Id);

                authorOld.Name = newAuthor.Name;
                authorOld.Surname = newAuthor.Surname;
                authorOld.Mail = newAuthor.Mail;
                authorOld.Birthday = newAuthor.Birthday;

                using (var transaction = this.Context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in authorOld.Books)
                        {
                            item.Author = authorOld;
                            item.authorName = authorOld.Name + " " + authorOld.Surname;
                            this.Context.Books.Update(item);
                        }
                        await this.Context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        transaction.Rollback();
                    }
                }

                Context.Authors.Update(authorOld);
                await this.Context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed();
            }

        }
    }
}
