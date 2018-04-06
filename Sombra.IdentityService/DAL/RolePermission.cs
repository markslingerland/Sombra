using System;
using Sombra.Core;

namespace Sombra.IdentityService
{
  public class RolePermission 
  {
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }

    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }
  }
}   