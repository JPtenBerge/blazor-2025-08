using System.ComponentModel.DataAnnotations;

namespace DemoProject.Entities;

public class Destination
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vul in stomme zak")]
    [RegularExpression("^[A-Z][a-zA-Z0-9 -]*$")]
    public string Location { get; set; }

    [Required]
    public int Rating { get; set; }

    [Required]
    public string PhotoUrl { get; set; }

    public IFormFile Description { get; set; }
}
