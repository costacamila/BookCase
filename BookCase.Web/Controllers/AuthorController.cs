using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCase.Domain.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BookCase.Web.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        // GET: AuthorController
        public ActionResult Index()
        {
            var client = new RestClient();
            var mail = this.HttpContext.Session.GetString("UserMail");
            var password = this.HttpContext.Session.GetString("UserPassword");

            var token = new RestRequest("https://localhost:5003/api/authenticate/token");
            token.AddJsonBody(JsonConvert.SerializeObject(new
            {
                Mail = mail,
                Password = password
            }));
            var tokenResponse = client.Post<TokenResult>(token).Data;

            var requestAuthor = new RestRequest("https://localhost:5003/api/author");
            requestAuthor.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

            return View(client.Get<IEnumerable<Author>>(requestAuthor).Data);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var mail = this.HttpContext.Session.GetString("UserMail");
            var password = this.HttpContext.Session.GetString("UserPassword");

            var token = new RestRequest("https://localhost:5003/api/authenticate/token");
            token.AddJsonBody(JsonConvert.SerializeObject(new
            {
                Mail = mail,
                Password = password
            }));
            var tokenResponse = client.Post<TokenResult>(token).Data;

            var requestAuthor = new RestRequest("https://localhost:5003/api/author/" + id);
            requestAuthor.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

            return View(client.Get<Author>(requestAuthor).Data);
        }

        // GET: AuthorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Author author)
        {
            try
            {
                var client = new RestClient();
                var mail = this.HttpContext.Session.GetString("UserMail");
                var password = this.HttpContext.Session.GetString("UserPassword");

                var token = new RestRequest("https://localhost:5003/api/authenticate/token");
                token.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Mail = mail,
                    Password = password
                }));
                var tokenResponse = client.Post<TokenResult>(token).Data;

                var requestAuthor = new RestRequest("https://localhost:5003/api/author");
                requestAuthor.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Name = author.Name,
                    Surname = author.Surname,
                    Mail = author.Mail,
                    Birthday = author.Birthday
                }));
                requestAuthor.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

                await client.PostAsync<Author>(requestAuthor);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("APP_ERROR", "Author already exists.");
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var mail = this.HttpContext.Session.GetString("UserMail");
            var password = this.HttpContext.Session.GetString("UserPassword");

            var token = new RestRequest("https://localhost:5003/api/authenticate/token");
            token.AddJsonBody(JsonConvert.SerializeObject(new
            {
                Mail = mail,
                Password = password
            }));
            var tokenResponse = client.Post<TokenResult>(token).Data;

            var requestAuthor = new RestRequest("https://localhost:5003/api/author/" + id);
            requestAuthor.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

            var author = (client.Get<Author>(requestAuthor).Data);
            this.HttpContext.Session.SetString("AuthorId", JsonConvert.SerializeObject(client.Get<Author>(requestAuthor).Data.Id.ToString()));
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Author author)
        {
            try
            {
                var client = new RestClient();
                var mail = this.HttpContext.Session.GetString("UserMail");
                var password = this.HttpContext.Session.GetString("UserPassword");

                var token = new RestRequest("https://localhost:5003/api/authenticate/token");
                token.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Mail = mail,
                    Password = password
                }));
                var tokenResponse = client.Post<TokenResult>(token).Data;

                var id = JsonConvert.DeserializeObject(this.HttpContext.Session.GetString("AuthorId"));

                Console.WriteLine(id);

                var requestAuthor = new RestRequest("https://localhost:5003/api/author/edit/");
                requestAuthor.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Id = id,
                    Name = author.Name,
                    Surname = author.Surname,
                    Mail = author.Mail,
                    Birthday = author.Birthday
                }));
                requestAuthor.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

                await client.PutAsync<Author>(requestAuthor);

                this.HttpContext.Session.Remove("AuthorId");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "Author doesn't exists.");
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var mail = this.HttpContext.Session.GetString("UserMail");
            var password = this.HttpContext.Session.GetString("UserPassword");

            var token = new RestRequest("https://localhost:5003/api/authenticate/token");
            token.AddJsonBody(JsonConvert.SerializeObject(new
            {
                Mail = mail,
                Password = password
            }));
            var tokenResponse = client.Post<TokenResult>(token).Data;

            var requestAuthor = new RestRequest("https://localhost:5003/api/author/" + id);
            requestAuthor.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

            var author = (client.Get<Author>(requestAuthor).Data);
            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAuthor([FromRoute] Guid id)
        {
            try
            {
                var client = new RestClient();
                var mail = this.HttpContext.Session.GetString("UserMail");
                var password = this.HttpContext.Session.GetString("UserPassword");

                var token = new RestRequest("https://localhost:5003/api/authenticate/token");
                token.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Mail = mail,
                    Password = password
                }));
                var tokenResponse = client.Post<TokenResult>(token).Data;

                var requestAuthor = new RestRequest("https://localhost:5003/api/author/delete/" + id);
                requestAuthor.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

                await client.DeleteAsync<Author>(requestAuthor);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "Author doesn't exists.");
                return View();
            }
        }
    }
}
