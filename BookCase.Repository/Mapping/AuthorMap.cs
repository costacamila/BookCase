using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookCase.Repository.Mapping
{
    public class AuthorMap : IEntityTypeConfiguration<Domain.Author.Author>
    {
        public void Configure(EntityTypeBuilder<Domain.Author.Author> builder)
        {
            builder.ToTable("Author");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Mail).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Birthday).IsRequired();
            builder.HasMany<Domain.Book.Book>(x => x.Books).WithOne(x => x.Author);

        }
    }
}
