using BookCase.Domain.Author.Repository;
using BookCase.Domain.Book.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCase.Service.BookService
{
    public class BookService : IBookService
    {
        private IBookRepository BookRepository { get; set; }
        private IAuthorRepository AuthorRepository { get; set; }

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.BookRepository = bookRepository;
            this.AuthorRepository = authorRepository;
        }

        public async Task<IdentityResult> SaveBook(Domain.Book.Book book)
        {
            var author = AuthorRepository.GetAll().Where(x => (x.Name + x.Surname).ToLower().Replace(" ", "") == book.authorName.ToLower().Replace(" ", "")).FirstOrDefault();
            if (BookRepository.GetBookByISBN(book.ISBN) != null)
            {
                throw new Exception("Já existe um livro com este ISBN");
            }
            else if (author == null) 
            {
                throw new Exception("Não existe um autor com este nome.");
            }
            book.Author = author;
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
            if (this.BookRepository.GetAll().Where(x => x.Id == newBook.Id).FirstOrDefault() == null)
            {
                throw new Exception("Este livro não existe");
            }
            return await this.BookRepository.UpdateBookAsync(newBook);
        }

        public async Task<IdentityResult> DeleteBookAsync(Guid id)
        {
            if (this.BookRepository.GetAll().Where(x => x.Id == id).FirstOrDefault() == null)
            {
                throw new Exception("Este livro não existe");
            }
            return await this.BookRepository.DeleteBookAsync(id);
        }
    }
}
