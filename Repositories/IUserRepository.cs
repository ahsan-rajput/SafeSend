namespace SafeSend.Repositories;

public interface IUserRepository
{
    Task<bool> IsEmailAvailableAsync(string email);
}
