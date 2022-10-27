using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrivateEvents.Entities.Models;
using PrivateEvents.Entities.Configuration;

namespace PrivateEvents.Entities;

public class RepositoryContext : IdentityDbContext<User> 
{
    public RepositoryContext(DbContextOptions options) 
    : base (options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

}