using Microsoft.AspNetCore.Mvc;
using Sombra.Web.Infrastructure.Authentication;

namespace Sombra.Web.Infrastructure
{
    public abstract class SombraController : Controller
    {
        public new SombraPrincipal User => base.User != null ? (SombraPrincipal) base.User : default;
    }
}