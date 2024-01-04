namespace HotelListAPI.DTO;

public class CountryDTO
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string ShortName { get; set; }
  public List<HotelSummaryDTO> Hotels { get; set; }
}