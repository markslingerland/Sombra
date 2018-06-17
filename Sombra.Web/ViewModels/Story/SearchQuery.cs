namespace Sombra.Web.ViewModels.Story
{
    public class SearchQuery : SubdomainViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 9;
    }
}