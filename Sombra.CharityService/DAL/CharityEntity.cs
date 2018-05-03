using Sombra.Infrastructure.DAL;
using Sombra.Messaging.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.CharityService.DAL
{
    public class CharityEntity : Entity
    {
        // TODO at more data relevant for charity
        public string CharityId { get; set; }
        public string NameCharity { get; set; }
        public string NameOwner { get; set; }
    }
}
