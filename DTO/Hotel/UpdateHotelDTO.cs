namespace HotelListAPI.DTO;

public class UpdateHotelDTO
{
  public string? Name { get; set; }
  public string? Address { get; set; }
  public double? Rating { get; set; }
  public int? CountryId { get; set; }
}