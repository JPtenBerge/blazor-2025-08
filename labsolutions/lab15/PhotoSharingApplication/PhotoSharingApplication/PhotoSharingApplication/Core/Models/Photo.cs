using PhotoSharingApplication.Client.Core.Models;

namespace PhotoSharingApplication.Core.Models;

public class Photo
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public byte[]? PhotoFile { get; set; }
    public string? ImageMimeType { get; set; }
    public string? DataUrl => PhotoFile == null ? null : $"data:{ImageMimeType};base64,{Convert.ToBase64String(PhotoFile)}";

    public List<Comment> Comments { get; set; } = [];
}
