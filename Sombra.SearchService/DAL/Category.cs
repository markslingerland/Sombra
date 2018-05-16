using Sombra.Infrastructure.DAL;

namespace Sombra.SearchService.DAL
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual Content Content { get; set; }

    }
}