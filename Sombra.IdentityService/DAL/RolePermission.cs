using Sombra.Core;

namespace Sombra.IdentityService
{
  public class RolePermission : Entity
  {
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }
  }
}   