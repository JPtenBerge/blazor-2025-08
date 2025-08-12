using DemoProject.Entities;
using DemoProject.Repositories;
using Microsoft.AspNetCore.Components;

namespace DemoProject.Components.Pages;

public partial class Holiday
{
    private readonly IDestinationRepository _destinationRepository;

    [SupplyParameterFromForm]
    public Destination NewDestination { get; set; } = new();

    public static List<Destination>? Destinations { get; set; }

    public Holiday(IDestinationRepository destinationRepository)
    {
        _destinationRepository = destinationRepository;
    }

    protected override async Task OnInitializedAsync()
    {
        Destinations = (await _destinationRepository.GetAllAsync()).ToList();
    }

    async Task AddDestination()
    {
        Console.WriteLine($"wow! Werkt! {NewDestination.Location}");

        using var stream = NewDestination.Description.OpenReadStream();
        using var reader = new StreamReader(stream);
        var descriptionContent = await reader.ReadToEndAsync();
        Console.WriteLine($"Heb ik description?? {descriptionContent}");

        await _destinationRepository.AddAsync(NewDestination);
        Destinations!.Add(NewDestination);

        // low coupling, high cohesion
    }
}
