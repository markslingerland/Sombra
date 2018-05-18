using Sombra.Infrastructure.DAL;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sombra.CharityActionService.DAL
{
    public class UserKey : Entity
    {
        public virtual CharityAction CharityAction { get; set; }
        public Guid CharityActionId { get; set; }
        public Guid Key { get; set; }
    }
}
