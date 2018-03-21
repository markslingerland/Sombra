using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Sombra.Messaging;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Core;
using System.Linq;
using System;

namespace Sombra.IdentityService
{
    public class UserLoginRequestHandler : IAsyncRequestHandler<UserLoginRequest, UserLoginResponse>
    {
        private readonly AuthenticationContext _context;
        public UserLoginRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<UserLoginResponse> Handle(UserLoginRequest message)
        {
            var response = new UserLoginResponse();

            CredentialType credentialType = _context.CredentialTypes.FirstOrDefault(ct => string.Equals(ct.Code, message.loginTypeCode, StringComparison.OrdinalIgnoreCase));

            if (credentialType == null)
                return null;

            Credential credential = _context.Credentials.FirstOrDefault(
                c => c.CredentialTypeId == credentialType.Id && string.Equals(c.Identifier, message.identifier, StringComparison.OrdinalIgnoreCase) && c.Secret == SHA256Hasher.ComputeHash(secret)
            );

            if (credential == null)
                return null;

            response.User = _context.Users.Find(credential.UserId);
            response.Success = true;

            return response;

        }
    }
}