using System.Threading.Tasks;
using Sombra.Core;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Infrastructure;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Sombra.IdentityService
{
    public class ChangePasswordRequestHandler : IAsyncRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
    {
        private readonly AuthenticationContext _context;
        public ChangePasswordRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest message)
        {
            
            ExtendedConsole.Log("ChangePasswordRequest received");
            var response = new ChangePasswordResponse(false);

            var credential = await _context.Credentials.FirstOrDefaultAsync(c => c.SecurityToken == message.SecurityToken && c.ExpirationDate > DateTime.UtcNow); 
            if(credential != null){
                credential.Secret = message.Password;
                credential.SecurityToken = string.Empty;
                _context.SaveChanges();
                response.Success = true;
            }           

            return response;
        }
    }
}
