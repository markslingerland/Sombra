using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.User;
using Sombra.Messaging.Responses.User;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class CreateUserRequestHandler : IAsyncRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public CreateUserRequestHandler(UserContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest message)
        {
            var user = _mapper.Map<User>(message);
            if (user.UserKey == default)
            {
                return new CreateUserResponse
                {
                    ErrorType = ErrorType.InvalidKey
                };
            }

            var isEmailAddressUniqueHandler = new UserEmailExistsRequestHandler(_context);
            var request = new UserEmailExistsRequest
            {
                CurrentUserKey = message.UserKey,
                EmailAddress = message.EmailAddress
            };

            var response = await isEmailAddressUniqueHandler.Handle(request);
            if (response.EmailExists)
            {
                return new CreateUserResponse
                {
                    ErrorType = ErrorType.EmailExists
                };
            }

            _context.Users.Add(user);

            if (!await _context.TrySaveChangesAsync()) return new CreateUserResponse();

            var userCreatedEvent = _mapper.Map<UserCreatedEvent>(user);
            await _bus.PublishAsync(userCreatedEvent);

            return CreateUserResponse.Success();
        }
    }
}