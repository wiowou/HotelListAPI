using System.ComponentModel.DataAnnotations;

namespace HotelListAPI.DTO;

public class CreateCountryDTO
{
  [Required]
  public string Name { get; set; }
  [Required]
  public string ShortName { get; set; }
}