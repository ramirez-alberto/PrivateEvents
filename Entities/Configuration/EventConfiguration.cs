using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateEvents.Entities.Models;

namespace PrivateEvents.Entities.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasData(
            new Event 
            {
                Name = "Baja Wines",
                OnDate = new DateTime(2022,11,15,12,0,0),
                Location = "Wine Ranch"
            },
            new Event 
            {
                Name = "Hack-A-Mole",
                OnDate = new DateTime(2022,11,15,12,0,0),
                Location = "El Paso Convention Center"
            },
            new Event 
            {
                Name = "Ontario Fest",
                OnDate = new DateTime(2022,11,15,12,0,0),
                Location = "The Canadian Club"
            },
            
            new Event 
            {
                Name = "Beer Fest 2030",
                OnDate = new DateTime(2030,11,15,12,0,0),
                Location = "Beer Garden"
            },
            
            new Event 
            {
                Name = "Rosarito Trip",
                OnDate = new DateTime(2023,06,13,1,05,0),
                Location = "Rosarito Beach"
            }           
        );
    }
}