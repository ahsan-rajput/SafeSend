namespace SafeSend.Repositories;

public record ShareLinkSummary(int Total, int Active, int ExpiredOrRevoked);

public interface IShareLinkSummaryRepository
{
    Task<ShareLinkSummary> GetSummaryAsync(string ownerId);
}
