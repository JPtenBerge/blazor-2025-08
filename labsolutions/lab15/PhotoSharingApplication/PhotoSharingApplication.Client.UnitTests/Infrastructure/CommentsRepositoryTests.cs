using FluentAssertions;
using PhotoSharingApplication.Client.Core.Models;
using PhotoSharingApplication.Client.Infrastructure;
using RichardSzalay.MockHttp;
using System.Text.Json;

namespace PhotoSharingApplication.Client.UnitTests.Infrastructure;

public class CommentsRepositoryTests
{
    private readonly HttpClient httpClient;
    private readonly MockHttpMessageHandler mockHttpHandler;
    public CommentsRepositoryTests()
    {
        mockHttpHandler = new MockHttpMessageHandler();
        httpClient = mockHttpHandler.ToHttpClient();
        httpClient.BaseAddress = new Uri("http://localhost");
    }

    [Fact]
    public async Task AddCommentAsync_ReturnsComment()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        mockHttpHandler.When(HttpMethod.Post, "http://localhost/api/comments").Respond("application/json", JsonSerializer.Serialize(comment));
        var sut = new CommentsRepository(httpClient);

        // Act
        var result = await sut.AddCommentAsync(comment);

        // Assert
        result.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task DeleteCommentAsync_ReturnsNoContent()
    {
        // Arrange
        mockHttpHandler.When(HttpMethod.Delete, "http://localhost/api/comments/1").Respond("application/json", "");
        var sut = new CommentsRepository(httpClient);

        // Act
        await sut.DeleteCommentAsync(1);

        // Assert
        mockHttpHandler.VerifyNoOutstandingRequest();
    }

    [Fact]
    public async Task GetCommentByIdAsync_ReturnsComment()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        mockHttpHandler.When(HttpMethod.Get, "http://localhost/api/comments/1").Respond("application/json", JsonSerializer.Serialize(comment));
        var sut = new CommentsRepository(httpClient);

        // Act
        var result = await sut.GetCommentByIdAsync(1);

        // Assert
        result.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task GetCommentsForPhotoAsync_ReturnsComments()
    {
        // Arrange
        var comments = new List<Comment>
        {
            new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" },
            new Comment { Id = 2, PhotoId = 1, Subject = "Test", Body = "Test" }
        };
        mockHttpHandler.When(HttpMethod.Get, "http://localhost/api/photos/1/comments").Respond("application/json", JsonSerializer.Serialize(comments));
        var sut = new CommentsRepository(httpClient);

        // Act
        var result = await sut.GetCommentsForPhotoAsync(1);

        // Assert
        result.Should().BeEquivalentTo(comments);
    }

    [Fact]
    public async Task UpdateCommentAsync_ReturnsComment()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        mockHttpHandler.When(HttpMethod.Put, "http://localhost/api/comments/1").Respond("application/json", JsonSerializer.Serialize(comment));
        var sut = new CommentsRepository(httpClient);

        // Act
        var result = await sut.UpdateCommentAsync(comment);

        // Assert
        result.Should().BeEquivalentTo(comment);
    }

}
