namespace PhotoSharingApplication.Client.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
