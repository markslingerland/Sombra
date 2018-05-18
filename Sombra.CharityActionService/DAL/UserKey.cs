using Sombra.Infrastructure.DAL;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sombra.CharityActionService.DAL
{
    public class UserKey : Entity
    {
        
        public Guid Key { get; set; }
    }
}
