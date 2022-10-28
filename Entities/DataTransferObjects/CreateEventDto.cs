using System.ComponentModel.DataAnnotations;

namespace PrivateEvents.Entities.DataTransferObjects;

public class CreateEventDto
{
    [Required]
    [MaxLength(20, ErrorMessage = "{0} can have a max of {1} characters")]
    public string? Name { get; set; }

    [DisplayFormat(NullDisplayText = "No Description")]
    [StringLength(100, ErrorMessage = "Maximum length for the Description is 100 characters.")]
    public string? Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Event Date")]
    public DateTime OnDate { get; set; }

    [Required]
    [StringLength(50)]
    public string? Location { get; set; }
}