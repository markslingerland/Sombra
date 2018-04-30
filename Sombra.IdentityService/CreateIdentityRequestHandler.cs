﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using CredentialType = Sombra.Core.Enums.CredentialType;

namespace Sombra.IdentityService
{
    public class CreateIdentityRequestHandler : IAsyncRequestHandler<CreateIdentityRequest, CreateIdentityResponse>
    {
        private readonly AuthenticationContext _context;

        public CreateIdentityRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<CreateIdentityResponse> Handle(CreateIdentityRequest message)
        {
            if (message.UserKey == default) return new CreateIdentityResponse();

            if (message.CredentialType == CredentialType.Email)
            {
                if (await _context.Credentials.AnyAsync(c => c.CredentialType.Name == CredentialType.Email && c.Identifier == message.Identifier))
                {
                    return new CreateIdentityResponse
                    {
                        ErrorType = ErrorType.EmailExists
                    };
                }
            }

            var activationToken = Hash.SHA256(Guid.NewGuid().ToString());

            var user = new User
            {
                ActivationToken = activationToken,
                Name = message.UserName,
                UserKey = message.UserKey,
                ActivationTokenExpirationDate = DateTime.UtcNow.AddDays(1)
            };

            var credential = new Credential
            {
                Identifier = message.Identifier,
                Secret = message.Secret,
                CredentialType = await _context.CredentialTypes.FirstOrDefaultAsync(c => c.Name == message.CredentialType),
                User = user
            };

            var userRole = new UserRole
            {
                Role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == message.Role),
                User = user
            };

            try
            {
                _context.UserRoles.Add(userRole);
                _context.Users.Add(user);
                _context.Credentials.Add(credential);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new CreateIdentityResponse();
            }

            return new CreateIdentityResponse
            {
                ActivationToken = activationToken,
                Success = true
            };
        }
    }
}