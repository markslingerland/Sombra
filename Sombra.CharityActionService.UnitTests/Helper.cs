using AutoMapper;
using Sombra.Infrastructure;

namespace Sombra.CharityActionService.UnitTests
{
    public static class Helper
    {
        public static IMapper GetMapper() => AutoMapperTestHelper.GetMapper(new MappingProfile());
    }
}