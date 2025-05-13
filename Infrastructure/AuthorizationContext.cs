using Authorization.Domain.Models;
using Authorization.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure;

public class AuthorizationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}