using AutoMapper;
using Sombra.Infrastructure.DAL;

namespace Sombra.Infrastructure.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreEntityProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
            where TDestination : IEntity
        {
            return expression.ForMember(d => d.Id, opt => opt.Ignore());
        }
    }
}
