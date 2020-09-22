using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCase.Repository.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<Domain.User.Role>
    {
        public void Configure(EntityTypeBuilder<Domain.User.Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}