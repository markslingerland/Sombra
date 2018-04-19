using AutoMapper;

namespace Sombra.Messaging.Infrastructure
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreMessageProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
            where TDestination : IMessage
        {
            return expression.ForMember(d => d.MessageCreated, opt => opt.Ignore())
                .ForMember(d => d.MessageType, opt => opt.Ignore());
        }

        public static IMappingExpression<TSource, TDestination> IgnoreResponseProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
            where TDestination : IResponse
        {
            return expression.IgnoreMessageProperties()
                .ForMember(d => d.IsRequestSuccessful, opt => opt.Ignore());
        }
    }
}