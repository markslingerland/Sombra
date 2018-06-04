using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses.Charity;

namespace Sombra.Messaging.Requests.Charity
{
    [Cachable]
    public class GetCharitiesRequest : PagedRequest<GetCharitiesResponse>
    {
        private bool _onlyApproved;
        private bool _onlyUnApproved;

        [CacheKey]
        public List<string> Keywords { get; set; }

        [CacheKey]
        public SortOrder SortOrder { get; set; }

        [CacheKey]
        public Category Category { get; set; }

        [CacheKey]
        public bool OnlyApproved
        {
            get => _onlyApproved;
            set
            {
                _onlyUnApproved = !value && _onlyUnApproved;
                _onlyApproved = value;
            }
        }

        [CacheKey]
        public bool OnlyUnapproved
        {
            get => _onlyUnApproved;
            set
            {
                _onlyApproved = !value && _onlyApproved;
                _onlyUnApproved = value;
            }
        }
    }
}