using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Shared.Dtos;

public record DestinationGetAllResponseDto
{
    public IEnumerable<DestinationGetAllDestinationDto> Destinations { get; set; }

    // pagination
    public int NrOfPages { get; set; }
    public int CurrentPage { get; set; }
}

public record DestinationGetAllDestinationDto
{
    public int Id { get; set; }
    public string Location { get; set; }
    public int Rating { get; set; }
    public string PhotoUrl { get; set; }
}