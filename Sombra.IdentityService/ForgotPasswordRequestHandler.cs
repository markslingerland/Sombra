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
    public class ForgotPasswordRequestHandler : IAsyncRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>
    {
        private readonly AuthenticationContext _context;
        public ForgotPasswordRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<ForgotPasswordResponse> Handle(ForgotPasswordRequest message)
        {
            
            Console.WriteLine("ForgotPasswordRequest received");
            var response = new ForgotPasswordResponse();

            var credential = await _context.Credentials.Select(b => b).Where(c => c.Identifier == message.Email).FirstOrDefaultAsync(); 
            if(credential != null){
                var SecurityToken = Guid.NewGuid();
                credential.SecurityToken = SecurityToken; 
                _context.SaveChanges();
                response.Success = true;
                response.Secret = SecurityToken;
           }           

            return response;
        }
    }
}
