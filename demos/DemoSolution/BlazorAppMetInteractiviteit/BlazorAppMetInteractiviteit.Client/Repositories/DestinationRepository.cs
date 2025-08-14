using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using Flurl.Http;
using System.Net.Http.Json;

namespace BlazorAppMetInteractiviteit.Client.Repositories;

public class DestinationRepository : IDestinationRepository
{
    private readonly HttpClient _http;
    public DestinationRepository(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<Destination>> GetAllAsync()
    {
        Console.WriteLine("getting all via repo");
        return await "https://localhost:7059/api/destinations"
            .GetJsonAsync<IEnumerable<Destination>>();
    }

    public async Task<Destination?> GetAsync(int id)
    {
        return await $"https://localhost:7059/api/destinations/{id}".GetJsonAsync<Destination>();
    }

    public async Task AddAsync(Destination newDestination)
    {
        await "https://localhost:7059/api/destinations".PostJsonAsync(newDestination);

        //var request = new HttpRequestMessage { Headers = new Dictionary<string, string>() };
        //await _http.SendAsync()

        //var response = await _http.PostAsJsonAsync<Destination>("api/products", newDestination);
        //await response.Content.ReadFromJsonAsync<Destination>();
    }
}
