using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrivateEvents.Entities.Models;
using PrivateEvents.Entities.Configuration;
using Microsoft.AspNetCore.Identity;

namespace PrivateEvents.Entities;

public class RepositoryContext : IdentityDbContext<User> 
{
    public RepositoryContext(DbContextOptions options) 
    : base (options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles",t => t.ExcludeFromMigrations());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new AttendeeConfiguration());
    }

    public DbSet<Event>? Events {get; set;}
    public DbSet<Attendee>? Attendees {get; set;}

}