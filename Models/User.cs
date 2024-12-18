using Microsoft.AspNetCore.Identity;

namespace chatapp_blazor.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class User : IdentityUser
{
    public required string Name { get; set; }
}