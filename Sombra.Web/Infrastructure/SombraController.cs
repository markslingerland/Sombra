using Microsoft.AspNetCore.Mvc;
using Sombra.Web.Infrastructure.Authentication;

namespace Sombra.Web.Infrastructure
{
    public abstract class SombraController : Controller
    {
        public new SombraPrincipal User => HttpContext.GetUser();
    }
}