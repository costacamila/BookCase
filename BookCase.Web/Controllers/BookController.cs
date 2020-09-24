using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCase.Domain.Author;
using BookCase.Domain.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BookCase.Web.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        // GET: BookController
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

            var requestBook = new RestRequest("https://localhost:5003/api/book");
            requestBook.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

            return View(client.Get<IEnumerable<Book>>(requestBook).Data);
        }

        // GET: BookController/Details/5
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

            var requestBook = new RestRequest("https://localhost:5003/api/book/" + id);
            requestBook.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

            return View(client.Get<Book>(requestBook).Data);
        }

        // GET: BookController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book)
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

                var requestAuthor = new RestRequest("https://localhost:5003/api/author/getbyname/" + book.authorName.ToLower().Replace(" ", ""));
                requestAuthor.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

                var author = client.Get<Author>(requestAuthor).Data;

                book.Author = author;

                var requestBook = new RestRequest("https://localhost:5003/api/book");
                requestBook.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Year = book.Year,
                    authorName = book.authorName,
                    Author = book.Author
                }));
                requestBook.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

                await client.PostAsync<Book>(requestBook);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("APP_ERROR", "Book already exists or Author doesn't exists.");
                return View();
            }
        }

        // GET: BookController/Edit/5
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

            var requestBook = new RestRequest("https://localhost:5003/api/book/" + id);
            requestBook.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

            var book = (client.Get<Book>(requestBook).Data);
            this.HttpContext.Session.SetString("BookId", JsonConvert.SerializeObject(client.Get<Book>(requestBook).Data.Id.ToString()));
            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Book book)
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

                var id = JsonConvert.DeserializeObject(this.HttpContext.Session.GetString("BookId"));

                var requestBook = new RestRequest("https://localhost:5003/api/book/edit/");
                requestBook.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Id = id,
                    Title = book.Title,
                    Year = book.Year
                })); ;
                requestBook.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

                await client.PutAsync<Book>(requestBook);

                this.HttpContext.Session.Remove("BookId");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError("APP_ERROR", "Book doesn't exists.");
                return View();
            }
        }

        // GET: BookController/Delete/5
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

            var requestBook = new RestRequest("https://localhost:5003/api/book/" + id);
            requestBook.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

            var book = (client.Get<Book>(requestBook).Data);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBook([FromRoute] Guid id)
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

                var requestBook = new RestRequest("https://localhost:5003/api/book/delete/" + id);
                requestBook.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

                await client.DeleteAsync<Book>(requestBook);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("APP_ERROR", "Book doesn't exists.");
                return View();
            }
        }
    }
}
