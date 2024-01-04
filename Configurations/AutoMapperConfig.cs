using AutoMapper;
using HotelListAPI.Data;
using HotelListAPI.DTO;

namespace HotelListAPI.Configurations;

public class AutoMapperConfig : Profile
{
  public AutoMapperConfig()
  {
    //required so that when null properties are ignored, nullable ints dont get 
    //converted to 0. Do the same with DateTime, double, etc
    CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
    CreateMap<double?, double>().ConvertUsing((src, dest) => src ?? dest);
    CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

    CreateMap<CreateCountryDTO, Country>();
    //ignores all null properties in src using ForAllMembers
    CreateMap<UpdateCountryDTO, Country>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
    CreateMap<Country, CountrySummaryDTO>();
    CreateMap<Country, CountryDTO>();

    CreateMap<Hotel, HotelDTO>();
    CreateMap<Hotel, HotelSummaryDTO>();
    CreateMap<UpdateHotelDTO, Hotel>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
    CreateMap<CreateHotelDTO, Hotel>();
  }
}