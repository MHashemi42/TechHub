using AutoMapper;
using TechHub.Core.DTOs;
using TechHub.Core.Entities;

namespace TechHub.Infrastructure.Profiles;

internal class TagProfile : Profile
{
	public TagProfile()
	{
		CreateMap<Tag, TagDto>();
	}
}
