using Sombra.Core;

namespace Sombra.Messaging
{
    public abstract class PagedRequest<TResponse> : Request<TResponse>, IPagedModel where TResponse : class, IResponse
    {
        private int _pageSize = 20;
        private int _pageNumber = 1;

        [CacheKey]
        public int PageSize
        {
            get => _pageSize > 0 ? _pageSize : 20;
            set => _pageSize = value;
        }

        [CacheKey]
        public int PageNumber
        {
            get => _pageNumber > 0 ? _pageNumber : 1;
            set => _pageNumber = value;
        }
    }
}