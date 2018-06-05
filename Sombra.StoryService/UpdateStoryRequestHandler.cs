using System.Threading.Tasks;
using AutoMapper;
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

        public Task<UpdateStoryResponse> Handle(UpdateStoryRequest message)
        {
            throw new System.NotImplementedException();
        }
    }
}