using System;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
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
                ExtendedConsole.Log("CreateUserRequestHandler: UserKey is empty");
                return new CreateUserResponse
                {
                    ErrorType = ErrorType.InvalidUserKey
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

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new CreateUserResponse();
            }

            var userCreatedEvent = _mapper.Map<UserCreatedEvent>(user);
            await _bus.PublishAsync(userCreatedEvent);

            return new CreateUserResponse
            {
                Success = true
            };
        }
    }
}