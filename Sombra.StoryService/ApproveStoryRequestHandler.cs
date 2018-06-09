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
    public class ApproveStoryRequestHandler : IAsyncRequestHandler<ApproveStoryRequest, ApproveStoryResponse>
    {
        private readonly StoryContext _context;

        public ApproveStoryRequestHandler(StoryContext context)
        {
            _context = context;
        }

        public async Task<ApproveStoryResponse> Handle(ApproveStoryRequest message)
        {
            var story = await _context.Stories.FirstOrDefaultAsync(b => b.StoryKey.Equals(message.StoryKey));
            if (story != null)
            {
                if (story.IsApproved)
                    return new ApproveStoryResponse
                    {
                        ErrorType = ErrorType.AlreadyActive
                    };

                story.IsApproved = true;

                return await _context.TrySaveChangesAsync<ApproveStoryResponse>();
            }

            return new ApproveStoryResponse
            {
                ErrorType = ErrorType.NotFound
            };
        }
    }
}