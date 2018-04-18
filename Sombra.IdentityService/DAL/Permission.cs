using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL
{
  public class Permission : Entity
  {
    public Core.Enums.Permission Name { get; set; }

    public int? Position { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
  }
}