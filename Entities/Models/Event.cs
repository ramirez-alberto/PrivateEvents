using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateEvents.Entities.Models;

[Table("Events")]
public class Event
{
    public Event()
    {
        CreatedDate = DateTime.Now;
    }
    public int EventId { get; set; }

    [Required]
    [MaxLength(20, ErrorMessage = "{0} can have a max of {1} characters")]
    public string? Name { get; set;}

    [DisplayFormat(NullDisplayText = "No Description")]
    [StringLength(100, ErrorMessage = "Maximum length for the Description is 100 characters.")]
    public string? Description { get; set;}
    
    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Event Date")]
    public DateTime OnDate { get; set;}
    
    [Required]
    [StringLength(50)]
    public string? Location { get; set;}
    public DateTime CreatedDate { get; set;}

    [ForeignKey(nameof(User))]
    public string Author {get; set;}

    public User User {get; set;}
}