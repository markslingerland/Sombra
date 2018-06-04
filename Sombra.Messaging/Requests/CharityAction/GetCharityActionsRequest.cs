using System;
using System.Collections.Generic;
using Sombra.Core.Enums;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.Messaging.Requests.CharityAction
{
    [Cachable(LifeTimeInHours = 1)]
    public class GetCharityActionsRequest : PagedRequest<GetCharityActionsResponse>
    {
        private bool _onlyApproved;
        private bool _onlyUnApproved;

        [CacheKey]
        public Guid CharityKey { get; set; }

        [CacheKey]
        public bool OnlyActive { get; set; }

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