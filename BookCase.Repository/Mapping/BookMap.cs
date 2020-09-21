using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookCase.Repository.Mapping
{
    public class BookMap : IEntityTypeConfiguration<Domain.Book.Book>
    {
        public void Configure(EntityTypeBuilder<Domain.Book.Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ISBN).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Year).IsRequired().HasMaxLength(4);
            builder.Property(x => x.authorName).IsRequired().HasMaxLength(100);
            builder.HasOne<Domain.Author.Author>(x => x.Author);

        }
    }
}
