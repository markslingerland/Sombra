using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class UpdateUserRequestHandler : IAsyncRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UpdateUserRequestHandler(UserContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest message)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserKey == message.UserKey);
            if (existingUser == null)
            {
                return new UpdateUserResponse
                {
                    Success = false,
                    ErrorType = UpdateUserErrorType.NotFound
                };
            }

            _context.Entry(existingUser).CurrentValues.SetValues(message);
            await _context.SaveChangesAsync();

            var userUpdatedEvent = _mapper.Map<UserUpdatedEvent>(existingUser);
            await _bus.PublishAsync(userUpdatedEvent);

            return new UpdateUserResponse
            {
                Success = true
            };
        }
    }
}