using DemoProject.Entities;

namespace DemoProject.Repositories;

public interface IDestinationRepository
{
    Task AddAsync(Destination newDestination);
    Task<IEnumerable<Destination>> GetAllAsync();
}