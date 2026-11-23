using Microsoft.AspNetCore.Identity;
using SafeSend.Models;

namespace SafeSend.Repositories;

public class UserRepository(UserManager<ApplicationUser> userManager) : IUserRepository
{
    public async Task<bool> IsEmailAvailableAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email.Trim());
        return user is null;
    }
}
