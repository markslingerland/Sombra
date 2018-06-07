using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class CreateStoryRequestHandler : IAsyncRequestHandler<CreateStoryRequest, CreateStoryResponse>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public CreateStoryRequestHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateStoryResponse> Handle(CreateStoryRequest message)
        {
            var story = _mapper.Map<Story>(message);
            if (story.StoryKey == default)
            {
                return new CreateStoryResponse {
                    ErrorType = ErrorType.InvalidKey
                };
            }

            if (message.AuthorUserKey.HasValue)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserKey == message.AuthorUserKey);
                if (user != null)
                {
                    story.Author = user;
                }
                else
                {
                    return new CreateStoryResponse
                    {
                        ErrorType = ErrorType.InvalidUserKey
                    };
                }
            }

            _context.Stories.Add(story);

            return await _context.TrySaveChangesAsync<CreateStoryResponse>();
        }
    }
}