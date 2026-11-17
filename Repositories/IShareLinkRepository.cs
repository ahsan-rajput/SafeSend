using SafeSend.Models;

namespace SafeSend.Repositories;

public interface IShareLinkRepository
{
    Task<IReadOnlyList<ShareLink>> GetByOwnerAsync(string ownerId);
}
