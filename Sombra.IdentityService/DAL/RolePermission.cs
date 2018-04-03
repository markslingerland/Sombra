using System;

namespace Sombra.IdentityService.DAL
{
  public class RolePermission 
  {
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }

    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }
  }
}   