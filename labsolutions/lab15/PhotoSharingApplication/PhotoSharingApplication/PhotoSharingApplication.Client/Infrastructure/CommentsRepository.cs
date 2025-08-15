using PhotoSharingApplication.Client.Core.Interfaces;
using PhotoSharingApplication.Client.Core.Models;
using System.Net.Http.Json;

namespace PhotoSharingApplication.Client.Infrastructure;

public class CommentsRepository(HttpClient httpClient) : ICommentsRepository
{
    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("/apiauth/comments", comment);
        return await response.Content.ReadFromJsonAsync<Comment>();
    }

    public async Task DeleteCommentAsync(int id)
    {
        await httpClient.DeleteAsync($"/apiauth/comments/{id}");
    }

    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<Comment>($"/api/comments/{id}");
    }

    public async Task<List<Comment>> GetCommentsForPhotoAsync(int photoId)
    {
        return await httpClient.GetFromJsonAsync<List<Comment>>($"/api/photos/{photoId}/comments");
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        HttpResponseMessage response = await httpClient.PutAsJsonAsync($"/apiauth/comments/{comment.Id}", comment);
        return comment;
    }
}
