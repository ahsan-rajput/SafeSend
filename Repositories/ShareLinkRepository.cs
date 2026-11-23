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
            FROM ShareLinks WHERE OwnerId = @OwnerId
            ORDER BY CreatedAtUtc DESC;
            """;
        await using var connection = CreateConnection();
        var links = await connection.QueryAsync<ShareLink>(sql, new { OwnerId = ownerId });
        return links.ToList();
    }

    public async Task<ShareLink?> GetByIdAsync(int id, string ownerId)
    {
        const string sql = """
            SELECT Id, OwnerId, Label, CreatedAtUtc, ExpiresAtUtc, IsRevoked
            FROM ShareLinks WHERE Id = @Id AND OwnerId = @OwnerId LIMIT 1;
            """;
        await using var connection = CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<ShareLink>(sql, new { Id = id, OwnerId = ownerId });
    }

    public async Task<int> CreateAsync(ShareLink link)
    {
        const string sql = """
            INSERT INTO ShareLinks (OwnerId, Label, CreatedAtUtc, ExpiresAtUtc, IsRevoked)
            VALUES (@OwnerId, @Label, @CreatedAtUtc, @ExpiresAtUtc, 0);
            SELECT last_insert_rowid();
            """;
        await using var connection = CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, new
        {
            link.OwnerId,
            link.Label,
            CreatedAtUtc = link.CreatedAtUtc.ToString("O"),
            ExpiresAtUtc = link.ExpiresAtUtc.ToString("O"),
            link.IsRevoked
        });
    }

    public async Task<bool> RevokeAsync(int id, string ownerId)
    {
        const string sql = """
            UPDATE ShareLinks SET IsRevoked = 1
            WHERE Id = @Id AND OwnerId = @OwnerId AND IsRevoked = 0;
            """;
        await using var connection = CreateConnection();
        return await connection.ExecuteAsync(sql, new { Id = id, OwnerId = ownerId }) > 0;
    }
}
