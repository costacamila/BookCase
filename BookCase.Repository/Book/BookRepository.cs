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

        public Task<IdentityResult> CreateBookAsync(Domain.Book.Book author)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteBookAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> FindByIdAsync(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Book.Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public Domain.Book.Book GetById(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateBookAsync(Domain.Book.Book newBook)
        {
            throw new NotImplementedException();
        }

        public Domain.Book.Book GetBookByISBN(string ISBN)
        {
            return this.Context.Books.FirstOrDefault(x => x.ISBN == ISBN);
        }
    }
}
