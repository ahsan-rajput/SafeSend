using System.ComponentModel.DataAnnotations;

namespace SafeSend.Models;

public class CreateShareLinkViewModel
{
    [Required, StringLength(80, MinimumLength = 2)]
    public string Label { get; set; } = string.Empty;

    [Required]
    public DateTime ExpiresAtLocal { get; set; }

    public bool OneTimeAccess { get; set; } = true;
}
