using System.Collections.Generic;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Responses.Donate
{
    public class GetCharityActionsResponse : Response
    {
        public List<KeyNamePair> CharityActions { get; set; }
    }
}