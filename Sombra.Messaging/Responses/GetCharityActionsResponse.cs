using System.Collections.Generic;
using Sombra.Messaging.Shared;

namespace Sombra.Messaging.Responses
{
    public class GetCharityActionsResponse : Response
    {
        public List<CharityAction> Results { get; set; }
        public int TotalResult { get; set; }
    }
}