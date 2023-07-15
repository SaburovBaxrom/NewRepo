using AutoMapper;
using PracticeWebApi.Model.Domain;
using PracticeWebApi.Model.DTO;

namespace PracticeWebApi
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<Region, RegionDTO>().ReverseMap();
			CreateMap<UpdateRegionDto, Region>().ReverseMap();
			CreateMap<AddRegionDto, Region>().ReverseMap();

			CreateMap<Walk, WalkDto>().ReverseMap();
			CreateMap<AddWalkDto,Walk>().ReverseMap();
			CreateMap<Walk,UpdateWalkDto>().ReverseMap();

		}
	}
}
