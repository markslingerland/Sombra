using Sombra.Core;

namespace Sombra.IdentityService
{
  public class Permission : Entity
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
  }
}