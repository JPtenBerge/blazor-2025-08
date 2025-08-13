using Demo.Shared.Entities;
using Demo.Shared.Repositories;

namespace BlazorAppMetInteractiviteit.Client.Repositories;

public class DestinationRepository : IDestinationRepository
{
    public Task AddAsync(Destination newDestination)
    {
        throw new NotImplementedException();
    }

    public Task<Destination?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Destination>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
