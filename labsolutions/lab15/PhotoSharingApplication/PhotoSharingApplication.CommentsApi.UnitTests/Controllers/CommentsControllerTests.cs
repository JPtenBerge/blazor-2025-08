using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApplication.CommentsApi.Core.Interfaces;
using PhotoSharingApplication.CommentsApi.Core.Models;
using PhotoSharingApplication.CommentsApi.Controllers;

namespace PhotoSharingApplication.BUnitTests.Controllers;

public class CommentsControllerTests
{
    [Fact]
    public async Task GetComment_ReturnsAComment()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        var mockRepo = new Mock<ICommentsRepository>();
        mockRepo.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync(comment);
        var controller = new CommentsController(mockRepo.Object);

        // Act
        var result = await controller.GetComment(1);

        // Assert
        result.Value.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task GetComment_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<ICommentsRepository>();
        mockRepo.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync((Comment)null);
        var controller = new CommentsController(mockRepo.Object);

        // Act
        var result = await controller.GetComment(1);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetCommentsByPhotoId_ReturnsComments()
    {
        // Arrange
        var comments = new List<Comment>
        {
            new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" },
            new Comment { Id = 2, PhotoId = 1, Subject = "Test", Body = "Test" }
        };
        var mockRepo = new Mock<ICommentsRepository>();
        mockRepo.Setup(repo => repo.GetCommentsForPhotoAsync(1)).ReturnsAsync(comments);
        var controller = new CommentsController(mockRepo.Object);

        // Act
        var result = await controller.GetCommentsByPhotoId(1);

        // Assert
        result.Should().BeEquivalentTo(comments);
    }

    [Fact]
    public async Task PutComment_WithNonMatchingId_ReturnsBadRequest()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        var mockRepo = new Mock<ICommentsRepository>();
        var controller = new CommentsController(mockRepo.Object);

        // Act
        var result = await controller.PutComment(2, comment);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task PutComment_WithMatchingId_ReturnsNoContent()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        var mockRepo = new Mock<ICommentsRepository>();
        mockRepo.Setup(repo => repo.UpdateCommentAsync(comment));
        var controller = new CommentsController(mockRepo.Object);

        // Act
        var result = await controller.PutComment(1, comment);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task PostComment_ReturnsCreatedAtAction()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        var mockRepo = new Mock<ICommentsRepository>();
        mockRepo.Setup(repo => repo.AddCommentAsync(comment));
        var controller = new CommentsController(mockRepo.Object);

        // Act
        var result = await controller.PostComment(comment);

        // Assert
        var createdAtActionResult = result.Result as CreatedAtActionResult;
        createdAtActionResult.ActionName.Should().Be("GetComment");
        createdAtActionResult.RouteValues["id"].Should().Be(comment.Id);
        createdAtActionResult.Value.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task DeleteComment_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<ICommentsRepository>();
        mockRepo.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync((Comment)null);
        var controller = new CommentsController(mockRepo.Object);

        // Act
        var result = await controller.DeleteComment(1);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteComment_WithExistentId_ReturnsComment()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        var mockRepo = new Mock<ICommentsRepository>();
        mockRepo.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync(comment);
        var controller = new CommentsController(mockRepo.Object);

        // Act
        var result = await controller.DeleteComment(1);

        // Assert
        result.Value.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task DeleteComment_WithExistentId_DeletesComment()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        var mockRepo = new Mock<ICommentsRepository>();
        mockRepo.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync(comment);
        var controller = new CommentsController(mockRepo.Object);

        // Act
        var result = await controller.DeleteComment(1);

        // Assert
        mockRepo.Verify(repo => repo.DeleteCommentAsync(1), Times.Once);
    }
}
