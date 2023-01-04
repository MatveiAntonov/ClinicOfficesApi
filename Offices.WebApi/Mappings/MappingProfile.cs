using AutoMapper;
using Offices.Domain.Entities;
using Offices.WebApi.Models;

namespace Offices.WebApi.Mappings;

class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<OfficeDto, Office>().ReverseMap();
	}
}