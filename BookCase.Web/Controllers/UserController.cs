using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCase.Domain.User;
using BookCase.Service.UserService;
using BookCase.Web.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BookCase.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserService UserService { get; set; }
        private IUserIdentityManager UserIdentityManager { get; set; }

        public UserController(IUserService userService, IUserIdentityManager userIdentityManager)
        {
            this.UserService = userService;
            this.UserIdentityManager = userIdentityManager;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                var client = new RestClient();

                var requestUser = new RestRequest("https://localhost:5003/api/user");
                requestUser.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Mail = user.Mail,
                    Password = user.Password
                }));

                await client.PostAsync<User>(requestUser);

                return RedirectToAction("Login");
            }
            catch
            {
                ModelState.AddModelError("APP_ERROR", "Mail already being used.");
                return View(user);
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var result = await this.UserIdentityManager.Login(loginViewModel.Email, loginViewModel.Password);
                if (!result.Succeeded) { Redirect("/"); }

                var client = new RestClient();

                var token = new RestRequest("https://localhost:5003/api/authenticate/token");
                token.AddJsonBody(JsonConvert.SerializeObject(new
                {
                    Email = loginViewModel.Email,
                    Password = loginViewModel.Password
                }));

                var tokenResponse = client.Post<TokenResult>(token).Data;

                this.HttpContext.Session.SetString("TokenObject", JsonConvert.SerializeObject(tokenResponse.Token));


                var requestUser = new RestRequest("https://localhost:5003/api/user/getbyemail/" + loginViewModel.Email, DataFormat.Json);

                token.AddHeader("Authorization", "Bearer " + tokenResponse.Token);

                var user = client.Get<User>(requestUser).Data;

                this.HttpContext.Session.SetString("UserMail", user.Mail);
                this.HttpContext.Session.SetString("UserPassword", user.Password);
                this.HttpContext.Session.SetString("UserId", user.Id.ToString());

                return RedirectToAction("Index", "Author");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error, please try again.");
                return Redirect("/");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await this.UserIdentityManager.Logout();
            this.HttpContext.Session.Clear();
            return Redirect("Login");
        }

    }

    public class TokenResult
    {
        public String Token { get; set; }
    }

}
