using Microsoft.AspNetCore.Components;
using PhotoSharingApplication.Client.Core.Models;

namespace PhotoSharingApplication.Client.Components;

public class CommentComponentBase : ComponentBase
{
    [Parameter]
    public Comment? Comment { get; set; }
    protected Comment? comment;

    [Parameter]
    public CommentStates CommentState { get; set; } = CommentStates.View;

    [Parameter]
    public EventCallback<CommentStates> CommentStateChanged { get; set; }

    override protected void OnParametersSet()
    {
        comment = Comment is null ? null : new Comment() { Id = Comment.Id, PhotoId = Comment.PhotoId, Subject = Comment.Subject, Body = Comment.Body };
    }

    protected async Task NotifyCommentStateChanged(CommentStates state) => await CommentStateChanged.InvokeAsync(state);
}
