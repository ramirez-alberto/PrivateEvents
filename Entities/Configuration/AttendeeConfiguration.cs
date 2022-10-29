using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PrivateEvents.Entities.Models;


namespace PrivateEvents.Entities.Configuration;
public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> modelBuilder)
    {

    modelBuilder.HasKey(a => new { a.EventId, a.UserId });  

    modelBuilder.HasOne(a => a.User)
        .WithMany(u => u.Attendees)
        .HasForeignKey(a => a.UserId)
        .OnDelete(DeleteBehavior.Restrict);  

    modelBuilder.HasOne(a => a.Event)
        .WithMany(e => e.Attendees)
        .HasForeignKey(a => a.EventId)
        .OnDelete(DeleteBehavior.Restrict);

    }}