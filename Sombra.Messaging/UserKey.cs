using System;
using System.ComponentModel.DataAnnotations;


namespace Sombra.Messaging
{
    public class UserKey
    {
        [Key]
        public Guid Key { get; set; }
    }
}

