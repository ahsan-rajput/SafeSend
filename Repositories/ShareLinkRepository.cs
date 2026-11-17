using Dapper;
using Microsoft.Data.Sqlite;
using SafeSend.Models;

namespace SafeSend.Repositories;

public class ShareLinkRepository(IConfiguration configuration) : IShareLinkRepository
{
    private SqliteConnection CreateConnection() =>
        new(configuration.GetConnectionString("DefaultConnection"));

    public async Task<IReadOnlyList<ShareLink>> GetByOwnerAsync(string ownerId)
    {
        const string sql = """
            SELECT Id, OwnerId, Label, CreatedAtUtc, ExpiresAtUtc, IsRevoked
            FROM ShareLinks
            WHERE OwnerId = @OwnerId
            ORDER BY CreatedAtUtc DESC;
            """;
        await using var connection = CreateConnection();
        var links = await connection.QueryAsync<ShareLink>(sql, new { OwnerId = ownerId });
        return links.ToList();
    }
}
