namespace Sombra.Web.ViewModels.Shared
{
    public class PagedViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfResults { get; set; }
    }
}