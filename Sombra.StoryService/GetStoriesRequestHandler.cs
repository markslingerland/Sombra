using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.Core.Extensions;
using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class GetStoriesRequestHandler : IAsyncRequestHandler<GetStoriesRequest, GetStoriesResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public GetStoriesRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetStoriesResponse> Handle(GetStoriesRequest message)
        {
            Expression<Func<Story, bool>> filter = _ => true;
            if (message.CharityKey != default) filter = filter.And(s => s.CharityKey == message.CharityKey);
            if (message.OnlyApproved) filter = filter.And(s => s.IsApproved);
            if (message.OnlyUnapproved) filter = filter.And(s => !s.IsApproved);
            if (message.AuthorUserKey != default) filter = filter.And(s => s.Author.UserKey == message.AuthorUserKey);

            return new GetStoriesResponse
            {
                TotalNumberOfResults = _context.Stories.Count(filter),
                Results = await _context.Stories.IncludeAuthor().IncludeImages().Where(filter).OrderBy(t => t.Title, message.SortOrder)
                    .ProjectToPagedListAsync<Messaging.Shared.Story>(message, _mapper)
            };
        }
    }
}