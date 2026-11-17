using Dapper;
using Microsoft.Data.Sqlite;

namespace SafeSend.Data;

public class ShareLinkDbInitializer(IConfiguration configuration)
{
    public async Task InitializeAsync()
    {
        await using var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();
        const string sql = """
            CREATE TABLE IF NOT EXISTS ShareLinks (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                OwnerId TEXT NOT NULL,
                Label TEXT NOT NULL,
                CreatedAtUtc TEXT NOT NULL,
                ExpiresAtUtc TEXT NOT NULL,
                IsRevoked INTEGER NOT NULL DEFAULT 0
            );
            CREATE INDEX IF NOT EXISTS IX_ShareLinks_OwnerId ON ShareLinks(OwnerId);
            """;
        await connection.ExecuteAsync(sql);
    }
}
