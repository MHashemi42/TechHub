using AutoMapper;
using DNTPersianUtils.Core;
using TechHub.Core.DTOs;
using TechHub.Core.Entities;
using TechHub.Core.Helpers;
using TechHub.Infrastructure.Helpers;

namespace TechHub.Infrastructure.Profiles;

internal class PostProfile : Profile
{
    public PostProfile()
    {
        bool appendHhMm = true;
        bool includePersianDate = true;

        CreateMap<Post, PostSummaryDto>()
            .ForMember(p => p.PreviewContent,
                        m => m.MapFrom(p => HtmlHelpers.GetFirstParagraphFromHtml(p.Content)))
            .ForMember(p => p.DatePublished,
                        m => m.MapFrom(p => p.DatePublished
                        .ToFriendlyPersianDateTextify(appendHhMm, includePersianDate)));

        CreateMap<PagedList<Post>, PagedList<PostSummaryDto>>()
            .ConvertUsing<PagedListConverter<Post, PostSummaryDto>>();
    }
}
