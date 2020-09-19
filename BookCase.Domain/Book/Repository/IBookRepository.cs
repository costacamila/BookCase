using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Domain.Book.Repository
{
    public interface IBookRepository
    {
        Task<IdentityResult> CreateBookAsync(Domain.Book.Book author);
        Task<IdentityResult> UpdateBookAsync(Domain.Book.Book newBook);
        Task<IdentityResult> FindByIdAsync(Guid authorId);
        Task<IdentityResult> DeleteBookAsync(Guid id);

        IEnumerable<Domain.Book.Book> GetAll();
        Domain.Book.Book GetById(Guid authorId);
        Domain.Book.Book GetBookByISBN(string ISBN);
    }
}
