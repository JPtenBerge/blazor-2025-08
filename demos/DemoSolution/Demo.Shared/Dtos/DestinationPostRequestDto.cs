using Demo.Shared.Entities;

namespace DemoProject;

public record DestinationPostRequestDto
{
    public string Location { get; set; }
    public int Rating { get; set; }
    public string PhotoUrl { get; set; }
}

public record DestinationPostResponseDto
{
    public int Id { get; set; }
    public string Location { get; set; }
    public int Rating { get; set; }
    public string PhotoUrl { get; set; }
}

public static class DestinationPostRequestDtoExtensions
{
    public static Destination ToEntity(this DestinationPostRequestDto dto)
    {
        return new()
        {
            Location = dto.Location,
            PhotoUrl = dto.PhotoUrl,
            Rating = dto.Rating,
        };
    }

    public static DestinationPostResponseDto ToDto(this Destination entity)
    {
        return new()
        {
            Id = entity.Id,
            Location = entity.Location,
            PhotoUrl = entity.PhotoUrl,
            Rating = entity.Rating,
        };
    }
}