using System;
using Sombra.Core;

namespace Sombra.IdentityService
{
  public class UserRole
  {
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
  }
}