namespace HotelListAPI.DTO;

public class CreateHotelDTO
{
  public string Name { get; set; }
  public string Address { get; set; }
  public double Rating { get; set; }
  public int CountryId { get; set; }
}