using AutoMapper;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.Models;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LogsQuery, LogRequest>();
            CreateMap<Log, LogViewModel>();
            CreateMap<AuthenticationQuery, UserLoginRequest>();
            CreateMap<UserLoginResponse, LoginViewModel>();
            CreateMap<ForgotPasswordViewModel, ForgotPasswordRequest>();
            CreateMap<ForgotPasswordViewModel, GetUserByEmailRequest>();
        }
    }
}