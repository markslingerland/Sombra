using System;
using System.ComponentModel.DataAnnotations;

namespace Sombra.CharityActionService.DAL
{
    public class UserKey 
    {
        [Key]
        public Guid Key { get; set; }
    }
}
