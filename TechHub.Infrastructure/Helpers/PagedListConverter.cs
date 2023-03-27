using AutoMapper;
using TechHub.Core.Helpers;

namespace TechHub.Infrastructure.Helpers;

public class PagedListConverter<TIn, TOut> : ITypeConverter<PagedList<TIn>, PagedList<TOut>>
{
    public PagedList<TOut> Convert(PagedList<TIn> source, PagedList<TOut> destination,
        ResolutionContext context)
    {
        IEnumerable<TOut> items = context.Mapper.Map<IEnumerable<TOut>>(source);

        return new PagedList<TOut>(items, source.MetaData);
    }
}
