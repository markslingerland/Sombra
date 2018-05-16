using Sombra.Infrastructure.DAL;

namespace Sombra.SearchService.DAL
{
    public class Content : Entity
    {
        public System.Guid Key { get; set; }
        public string Name { get; set; }
        public Core.Enums.SearchContentType Type { get; set; }
        public string Description { get; set; }
    }
}