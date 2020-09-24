using BookCase.Domain.Book.Repository;
using BookCase.Repository.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Repository.Book
{
    public class BookRepository : IBookRepository
    {
        private BookCaseContext Context { get; set; }

        public BookRepository(BookCaseContext bookCaseContext)
        {
            this.Context = bookCaseContext;
        }

        public async Task<IdentityResult> CreateBookAsync(Domain.Book.Book book)
        {
            this.Context.Books.Add(book);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteBookAsync(Guid id)
        {
            this.Context.Books.Remove(GetById(id));
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task<IdentityResult> FindByIdAsync(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Book.Book> GetAll()
        {
            return this.Context.Books.AsEnumerable();
        }

        public Domain.Book.Book GetById(Guid bookId)
        {
            return this.Context.Books.FirstOrDefault(x => x.Id == bookId);
        }

        public async Task<IdentityResult> UpdateBookAsync(Domain.Book.Book newBook)
        {
            try
            {
                var bookOld = Context.Books.FirstOrDefault(x => x.Id == newBook.Id);

                bookOld.Title = newBook.Title;
                bookOld.Year = newBook.Year;

                Context.Books.Update(bookOld);
                await this.Context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return IdentityResult.Failed();
            }
            
        }

        public Domain.Book.Book GetBookByISBN(string ISBN)
        {
            return this.Context.Books.FirstOrDefault(x => x.ISBN == ISBN);
        }
    }
}
