using Microsoft.AspNetCore.Identity;

namespace SafeSend.Models;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
}
