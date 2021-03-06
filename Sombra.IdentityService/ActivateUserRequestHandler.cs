﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;

namespace Sombra.IdentityService
{
    public class ActivateUserRequestHandler : IAsyncRequestHandler<ActivateUserRequest, ActivateUserResponse>
    {
        private readonly AuthenticationContext _context;

        public ActivateUserRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<ActivateUserResponse> Handle(ActivateUserRequest message)
        {
            if (string.IsNullOrEmpty(message.ActivationToken))
                return new ActivateUserResponse
                {
                    ErrorType = ErrorType.TokenInvalid
                };

            var user = await _context.Users.FirstOrDefaultAsync(u => u.ActivationToken.Equals(message.ActivationToken));
            if (user != null)
            {
                if (DateTime.UtcNow > user.ActivationTokenExpirationDate)
                {
                    return new ActivateUserResponse
                    {
                        ErrorType = ErrorType.TokenExpired
                    };
                }

                user.IsActive = true;
                user.ActivationToken = null;

                return await _context.TrySaveChangesAsync<ActivateUserResponse>();
            }

            return new ActivateUserResponse
            {
                ErrorType = ErrorType.TokenInvalid
            };
        }
    }
}
