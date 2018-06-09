using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Story;
using Sombra.Messaging.Responses.Story;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class UpdateStoryRequestHandler : IAsyncRequestHandler<UpdateStoryRequest, UpdateStoryResponse>
    {
        private readonly StoryContext _context;

        public UpdateStoryRequestHandler(StoryContext context)
        {
            _context = context;
        }

        public async Task<UpdateStoryResponse> Handle(UpdateStoryRequest message)
        {
            var story = await _context.Stories.IncludeImages().FirstOrDefaultAsync(s => s.StoryKey == message.StoryKey);
            if (story == null)
            {
                return new UpdateStoryResponse
                {
                    ErrorType = ErrorType.NotFound
                };
            }

            _context.Entry(story).CurrentValues.SetValues(message);
            _context.Images.RemoveRange(story.Images);
            story.Images.Clear();
            
            if (message.Images != null)
            {
                var images = message.Images.Select(i => new Image { Base64 = i }).ToList();
                _context.Images.AddRange(images);
                story.Images = images;
            }

            return await _context.TrySaveChangesAsync<UpdateStoryResponse>();
        }
    }
}