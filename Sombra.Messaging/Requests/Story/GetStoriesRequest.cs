using System;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses.Story;

namespace Sombra.Messaging.Requests.Story
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetStoriesRequest : PagedRequest<GetStoriesResponse>
    {
        private bool _onlyApproved;
        private bool _onlyUnApproved;

        [CacheKey]
        public Guid AuthorUserKey { get; set; }

        [CacheKey]
        public Guid CharityKey { get; set; }

        [CacheKey]
        public SortOrder SortOrder { get; set; }

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