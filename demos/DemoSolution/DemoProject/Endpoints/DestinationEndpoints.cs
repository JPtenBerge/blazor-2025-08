using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel;

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

    public static async Task<IEnumerable<Destination>> GetAll(IDestinationRepository destinationRepo)
    {
        return await destinationRepo.GetAllAsync();
    }

    public static async Task<Results<NotFound<string>, Ok<Destination>>> Get(int id, IDestinationRepository destinationRepo)
    {
        var destination = await destinationRepo.GetAsync(id);
        return destination == null ? TypedResults.NotFound($"Destination ID {id} does not exist") : TypedResults.Ok(destination);
    }

    public static void Post(Destination newDestination, IDestinationRepository destinationRepo)
    {
        throw new NotImplementedException();
    }
}
