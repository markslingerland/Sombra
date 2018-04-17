using System.Collections.Generic;
using Sombra.Infrastructure.DAL;


namespace Sombra.IdentityService.DAL
{
  public class Role : Entity
  {
    public Core.Enums.Role Name { get; set; }

    public int? Position { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
  }
}