using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.CommentsApi.Core.Interfaces;
using PhotoSharingApplication.CommentsApi.Core.Models;
using PhotoSharingApplication.CommentsApi.Infrastructure.Data;

namespace PhotoSharingApplication.CommentsApi.Infrastructure.Repositories;

public class CommentsRepository(CommentsDbContext context) : ICommentsRepository
{
    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await context.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Comment>> GetCommentsForPhotoAsync(int photoId)
    {
        return await context.Comments.AsNoTracking().Where(c => c.PhotoId == photoId).ToListAsync();
    }

    public async Task DeleteCommentAsync(int id)
    {
        Comment? comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment is not null)
        {
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
        }
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        context.Comments.Update(comment);
        await context.SaveChangesAsync();
        return comment;
    }
}
