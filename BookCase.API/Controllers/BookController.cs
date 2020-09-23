using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCase.Domain.Book;
using BookCase.Service.BookService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookCase.API.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookService BookService { get; set; }

        public BookController(IBookService bookService)
        {
            this.BookService = bookService;
        }

        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return this.BookService.GetAll();
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public Book Get(Guid id)
        {
            return this.BookService.GetById(id);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IdentityResult> Post([FromBody] Book book)
        {
            var a = book.Author;
            return await this.BookService.SaveBook(book);
        }

        // PUT api/<BookController>/5
        [HttpPut("edit")]
        public async Task<IdentityResult> Put([FromBody] Book book)
        {
            return await this.BookService.UpdateBookAsync(book);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IdentityResult> Delete(Guid id)
        {
            return await this.BookService.DeleteBookAsync(id);
        }
    }
}
