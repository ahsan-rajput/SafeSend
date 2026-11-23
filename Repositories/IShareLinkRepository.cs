using SafeSend.Models;

namespace SafeSend.Repositories;

public interface IShareLinkRepository
{
    Task<IReadOnlyList<ShareLink>> GetByOwnerAsync(string ownerId);
    Task<ShareLink?> GetByIdAsync(int id, string ownerId);
    Task<int> CreateAsync(ShareLink link);
    Task<bool> RevokeAsync(int id, string ownerId);
}
