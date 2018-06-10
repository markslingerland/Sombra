using System.Collections.Generic;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Responses.Donate
{
    public class GetCharitiesResponse : Response
    {
        public List<KeyNamePair> Charities { get; set; }
    }
}