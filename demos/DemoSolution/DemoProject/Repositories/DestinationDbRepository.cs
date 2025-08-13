using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using DemoProject.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Repositories;

public class DestinationDbRepository : IDestinationRepository
{
    private readonly DemoContext _context;

    public DestinationDbRepository(DemoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Destination>> GetAllAsync() // meestal liever geen tabel dump methode
    {
        return await _context.Destinations.ToListAsync();
    }

    public async Task AddAsync(Destination newDestination)
    {
        _context.Destinations.Add(newDestination);
        await _context.SaveChangesAsync();
    }

    //public IEnumerable<Destination> GetTop3Best() // meestal liever geen tabel dump methode
    //{

    //}

    //public IEnumerable<Destination> GetByCountry() // meestal liever geen tabel dump methode
    //{

    //}

    //public IQueryable<Destination> GetAll4() // niet iedereens favoriet ANTI-PATTERN
    //{
    //    return context.Products.Where(x => !x.IsInactive)
    //}
}
