using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCase.Domain.User;
using BookCase.Service.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookCase.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; set; }

        public UserController(IUserService userService)
        {
            this.UserService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(Guid id)
        {
            return this.UserService.FindById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IdentityResult> Post([FromBody] User user)
        {
            return await this.UserService.Save(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("edit")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("delete/{id}")]
        public void Delete(Guid id)
        {
            var userToDelete = this.UserService.FindById(id);
            this.UserService.Delete(userToDelete);
        }

        [HttpGet("getbyemail/{email}")]
        public User GetByEmail([FromRoute] string email)
        {
            return this.UserService.GetUserByEmail(email);
        }
    }
}
