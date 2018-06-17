using System;
using System.Collections.Generic;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Responses.Donate
{
    public class GetCharitiesResponse : Response
    {
        public List<Charity> Charities { get; set; }
    }

    public class Charity 
    {
        public Guid CharityKey { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
    }
}