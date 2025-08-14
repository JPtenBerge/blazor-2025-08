using Demo.Shared.Dtos;
using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DemoProject.Endpoints;

public static class DestinationEndpoints
{
    public static void MapDestinationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/destinations").WithTags("Destinations");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", Get);
        group.MapPost("/", Post);
    }

    public static async Task<DestinationGetAllResponseDto> GetAll(IDestinationRepository destinationRepo)
    {
        return new()
        {
            Destinations = (await destinationRepo.GetAllAsync()).Select(d => new DestinationGetAllDestinationDto
            {
                Id = d.Id,
                Location = d.Location,
                PhotoUrl = d.PhotoUrl,
                Rating = d.Rating,
            }),
            NrOfPages = 5,
            CurrentPage = 1
        };

    }

    public static async Task<Results<NotFound<string>, Ok<DestinationGetResponseDto>>> Get(int id, IDestinationRepository destinationRepo)
    {
        var destination = await destinationRepo.GetAsync(id);
        throw new NotImplementedException();
        //return destination == null ? TypedResults.NotFound($"Destination ID {id} does not exist") : TypedResults.Ok(destination.ToDto());
    }

    public static async Task<Ok<DestinationPostResponseDto>> Post(DestinationPostRequestDto newDestination, IDestinationRepository destinationRepo)
    {
        var entity = newDestination.ToEntity();
        await destinationRepo.AddAsync(entity); // sets Id prop
        return TypedResults.Ok(entity.ToDto());
    }
}
