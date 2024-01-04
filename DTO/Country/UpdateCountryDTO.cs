using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HotelListAPI.DTO;

public class UpdateCountryDTO
{
  [Required]
  public string Name { get; set; }
  public string? ShortName { get; set; }
}