using Dapper;
using Microsoft.Data.Sqlite;

namespace SafeSend.Repositories;

public class ShareLinkSummaryRepository(IConfiguration configuration) : IShareLinkSummaryRepository
{
    public async Task<ShareLinkSummary> GetSummaryAsync(string ownerId)
    {
        const string sql = """
            SELECT
                COUNT(*) AS Total,
                COALESCE(SUM(CASE WHEN IsRevoked = 0 AND datetime(ExpiresAtUtc) > datetime(@Now) THEN 1 ELSE 0 END), 0) AS Active,
                COALESCE(SUM(CASE WHEN IsRevoked = 1 OR datetime(ExpiresAtUtc) <= datetime(@Now) THEN 1 ELSE 0 END), 0) AS ExpiredOrRevoked
            FROM ShareLinks WHERE OwnerId = @OwnerId;
            """;
        await using var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));
        return await connection.QuerySingleAsync<ShareLinkSummary>(sql, new
        {
            OwnerId = ownerId,
            Now = DateTime.UtcNow.ToString("O")
        });
    }
}
