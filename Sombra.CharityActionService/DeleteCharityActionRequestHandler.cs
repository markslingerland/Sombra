using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Events;
using System.Threading.Tasks;
using System.Linq;

namespace Sombra.CharityActionService
{
    public class DeleteCharityActionRequestHandler : IAsyncRequestHandler<DeleteCharityActionRequest, DeleteCharityActionResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public DeleteCharityActionRequestHandler(CharityActionContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<DeleteCharityActionResponse> Handle(DeleteCharityActionRequest message)
        {
            ExtendedConsole.Log("DeletedCharityActionRequest received");
            var charityAction = await _context.CharityActions.Include(b => b.UserKeys).FirstOrDefaultAsync(b => b.CharityActionkey.Equals(message.CharityActionkey));
            if (charityAction == null)
            {
                return new DeleteCharityActionResponse();
                
            }
            _context.CharityActions.Remove(charityAction);
            _context.UserKeys.RemoveRange(charityAction.UserKeys);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new DeleteCharityActionResponse();
            }
            var charityActionDeletedEvent = _mapper.Map<CharityActionDeletedEvent>(charityAction);
            await _bus.PublishAsync(charityActionDeletedEvent);

            return new DeleteCharityActionResponse() { Success = true };
            }
    }
}
