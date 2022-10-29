using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateEvents.Entities.Models;

public class Attendee
{
    public int EventId { get; set;}
    public Event Event { get; set;}
    public string UserId { get; set;} 
    public User User { get; set;}
}