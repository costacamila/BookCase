using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Service.BookService
{
    public interface IBookService
    {
        Task<IdentityResult> SaveBook(BookCase.Domain.Book.Book book);

        IEnumerable<Domain.Book.Book> GetAll();
        Domain.Book.Book GetById(Guid bookId);

        Task<IdentityResult> UpdateBookAsync(Domain.Book.Book newBook);
        Task<IdentityResult> DeleteBookAsync(Guid id);
    }
}
