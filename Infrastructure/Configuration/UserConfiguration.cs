using Authorization.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Password).IsRequired();
    }
}