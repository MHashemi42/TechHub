using AutoMapper;
using DNTPersianUtils.Core;
using TechHub.Core.DTOs;
using TechHub.Core.Entities;
using TechHub.Infrastructure.Helpers;

namespace TechHub.Infrastructure.Profiles;

internal class PostProfile : Profile
{
	public PostProfile()
	{
		CreateMap<Post, PostSummaryDto>()
			.ForMember(p => p.PreviewContent,
						m => m.MapFrom(p => HtmlHelpers.GetFirstParagraph(p.Content)))
			.ForMember(p => p.DatePublished,
						m => m.MapFrom(p => ToPersianDate(p.DatePublished)));
	}

	public string ToPersianDate(DateTime dateTime) => 
		dateTime.ToFriendlyPersianDateTextify(appendHhMm: true, convertToIranTimeZone: true, includePersianDate: false);
}
