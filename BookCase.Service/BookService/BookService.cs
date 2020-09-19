using BookCase.Domain.Book.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Service.BookService
{
    public class BookService : IBookService
    {
        private IBookRepository BookRepository { get; set; }

        public BookService(IBookRepository bookRepository)
        {
            this.BookRepository = bookRepository;
        }

        public async Task<IdentityResult> SaveBook(Domain.Book.Book book)
        {
            if (BookRepository.GetBookByISBN(book.ISBN) != null)
            {
                throw new Exception("Já existe um livro com este ISBN");
            }
            return await BookRepository.CreateBookAsync(book);
        }

        public IEnumerable<Domain.Book.Book> GetAll()
        {
            return BookRepository.GetAll();
        }

        public Domain.Book.Book GetById(Guid bookId)
        {
            return BookRepository.GetById(bookId);
        }

        public async Task<IdentityResult> UpdateBookAsync(Domain.Book.Book newBook)
        {
            return await this.BookRepository.UpdateBookAsync(newBook);
        }

        public async Task<IdentityResult> DeleteBookAsync(Guid id)
        {
            return await this.BookRepository.DeleteBookAsync(id);
        }
    }
}
