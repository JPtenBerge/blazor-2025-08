using Demo.Shared.Entities;

namespace Demo.Shared.Repositories;

public interface IDestinationRepository
{
    Task AddAsync(Destination newDestination);
    Task<IEnumerable<Destination>> GetAllAsync();
    Task<Destination?> GetAsync(int id);
}