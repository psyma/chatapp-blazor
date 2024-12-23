using Microsoft.AspNetCore.Identity;

namespace chatapp_blazor.Models;

public class User : IdentityUser
{
    public required string Name { get; set; }
}