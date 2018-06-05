using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Identity
{
    public class UpdateRolesResponse : CrudResponse
    {
        public Role Role { get; set; }
    }
}