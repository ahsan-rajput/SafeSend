namespace SafeSend.Models;

// Metadata only. Encrypted payloads and the browser-held decryption key are deliberately out of scope here.
public class ShareLink
{
    public int Id { get; set; }
    public string OwnerId { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
    public bool IsRevoked { get; set; }
}
