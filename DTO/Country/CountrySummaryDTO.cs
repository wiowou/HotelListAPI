using System.ComponentModel.DataAnnotations;

namespace HotelListAPI.DTO;

public class CountrySummaryDTO
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string ShortName { get; set; }
}