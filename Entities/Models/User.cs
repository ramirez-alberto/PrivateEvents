using Microsoft.AspNetCore.Identity;

namespace PrivateEvents.Entities.Models;

public class User :  IdentityUser
{
    public DateTime CreatedDate { get; set; }
}