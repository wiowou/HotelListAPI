namespace HotelListAPI.DTO;

public class HotelDTO
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public double Rating { get; set; }
  public CountrySummaryDTO Country { get; set; }
}