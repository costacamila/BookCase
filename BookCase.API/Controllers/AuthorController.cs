
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCase.Domain.Author;
using BookCase.Service.AuthorService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookCase.API.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService AuthorService { get; set; }

        public AuthorController(IAuthorService authorService)
        {
            this.AuthorService = authorService;
        }
        // GET: api/<AuthorController>
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            return this.AuthorService.GetAll();
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public Author Get(Guid id)
        {
            return this.AuthorService.GetById(id);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IdentityResult> Post([FromBody] Author author)
        {
            return await this.AuthorService.SaveAuthor(author);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("edit")]
        public async Task<IdentityResult> Put([FromBody] Author author)
        {
            return await this.AuthorService.UpdateAuthorAsync(author);
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IdentityResult> Delete([FromRoute] Guid id)
        {
            return await this.AuthorService.DeleteAuthorAsync(id);
        }

        [HttpGet("getbyname/{name}")]
        public Author Get([FromRoute] string name)
        {
            return this.AuthorService.GetAll().Where(x => (x.Name + x.Surname).Trim().ToLower() == name.Trim().ToLower()).FirstOrDefault();
        }
    }
}
