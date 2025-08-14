using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Shared.Dtos;

public record DestinationGetResponseDto
{
    public int Id { get; set; }
    public string Location { get; set; }
    public int Rating { get; set; }
    public string PhotoUrl { get; set; }
}
