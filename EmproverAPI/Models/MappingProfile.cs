using AutoMapper;
using EmproverAPI.Models.DB;
using EmproverAPI.Models.Dto;

namespace EmproverAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Point, PointDto>();
            CreateMap<PointDto, Point>();

            CreateMap<DayStatistics, DayStatisticsDto>()
                .ForMember(dest => dest.Point, opt => opt.MapFrom(src => src.Point));
            CreateMap<DayStatisticsDto, DayStatistics>()
                .ForMember(dest => dest.Point, opt => opt.MapFrom(src => src.Point));

            CreateMap<Symbol, SymbolDto>();
            CreateMap<SymbolDto, Symbol>();
            //CreateMap<User, UserDto>();
            //CreateMap<Indicator, IndicatorDto>();
            //CreateMap<IndicatorParameter, IndicatorParameterDto>();
            //CreateMap<AllowedValues, AllowedValuesDto>();
        }

    }
}
