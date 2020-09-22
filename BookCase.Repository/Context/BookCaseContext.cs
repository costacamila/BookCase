using BookCase.Repository.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookCase.Repository.Context
{
    public class BookCaseContext : DbContext
    {

        public DbSet<Domain.User.User> Users { get; set; }
        public DbSet<Domain.User.Role> Profiles { get; set; }
        public DbSet<Domain.Author.Author> Authors { get; set; }
        public DbSet<Domain.Book.Book> Books { get; set; }

        public static readonly ILoggerFactory _loggerFactory
                    = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public BookCaseContext(DbContextOptions<BookCaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("DataSource=app.db");
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AuthorMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new BookMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
