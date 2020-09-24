 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookCase.Domain.Author.Repository;
using BookCase.Domain.Book.Repository;
using BookCase.Domain.User.Repository;
using BookCase.Repository.Author;
using BookCase.Repository.Book;
using BookCase.Repository.User;
using BookCase.Service.Authenticate;
using BookCase.Service.AuthorService;
using BookCase.Service.BookService;
using BookCase.Service.UserService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BookCase.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<UserRepository>();

            services.AddControllers();

            services.AddDbContext<Repository.Context.BookCaseContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("ConnectionDB"));
            });


            services.AddTransient<AuthenticateService>();

            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = "Bearer";
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters.ValidIssuer = "BookCase-API";
                o.TokenValidationParameters.ValidAudience = "BookCase-API";
                o.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
