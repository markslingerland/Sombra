using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses.Identity
{
    public class UpdateRolesResponse : CrudResponse<UpdateRolesResponse>
    {
        public Role Role { get; set; }
    }
}