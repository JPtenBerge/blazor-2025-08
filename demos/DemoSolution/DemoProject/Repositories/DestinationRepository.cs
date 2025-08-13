using Demo.Shared.Entities;
using Demo.Shared.Repositories;

namespace DemoProject.Repositories;

public class DestinationRepository : IDestinationRepository
{
    private List<Destination> s_destinations { get; set; } =
    [
        new() {Id = 4, Location = "Garmisch-Partenkirchen", PhotoUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fimg.jamesedition.com%2Flisting_images%2F2025%2F08%2F05%2F16%2F27%2F22%2F4221ba56-cc8a-4d6a-960e-fd8e4330cc3a%2Fje%2F1040x620xc.jpg&f=1&nofb=1&ipt=4ee75d1fb1f1d694a03e560c8ad0335f55757801d59eabae7fd0655e684bff99", Rating = 8},
        new() {Id = 8, Location = "Van der Valk Veenendaal", PhotoUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fimg.hotelspecials.de%2F8cf72eff5ca009e6f371fdb3e68cbbdb.jpeg%3Fw%3D800%26h%3D528%26c%3D1%26quality%3D70&f=1&nofb=1&ipt=e40149718cd361915f265f1bfbb2ea2cd17ec29f2770e4b909038ad49120e6f5", Rating = 8},
        new() {Id = 15, Location = "Norilsk", PhotoUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fcdni.rbth.com%2Frbthmedia%2Fimages%2F2018.07%2Foriginal%2F5b5f09f815e9f96605429212.jpg&f=1&nofb=1&ipt=a9ecf64a564a4b806ae93fa0b81a17c673dfc6382bf23c1152de0886fb2251ea", Rating = 3},
    ];

    public Task<IEnumerable<Destination>> GetAllAsync() // meestal liever geen tabel dump methode
    {
        return Task.FromResult(s_destinations.AsEnumerable());
    }

    public Task AddAsync(Destination newDestination)
    {
        newDestination.Id = !s_destinations.Any() ? 1 : s_destinations.Max(x => x.Id) + 1;
        s_destinations.Add(newDestination);
        return Task.CompletedTask;
    }

    public Task<Destination?> GetAsync(int id)
    {
        throw new NotImplementedException();
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
