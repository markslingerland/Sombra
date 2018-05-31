namespace Sombra.Core
{
    public interface IPagedModel
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}