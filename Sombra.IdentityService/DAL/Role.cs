using System;
using System.Collections.Generic;
using Sombra.Infrastructure.DAL;


namespace Sombra.IdentityService.DAL
{
  public class Role : Entity
  {
    public Guid UserId { get; set; }
    public Core.Enums.Role RoleName { get; set; }
    
    public virtual User User { get; set; }

  }
}