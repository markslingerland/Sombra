using System.Collections.Generic;

namespace Sombra.Messaging.Responses.CharityAction
{
    public class GetCharityActionsResponse : Response
    {
        public List<Shared.CharityAction> Results { get; set; }
        public int TotalNumberOfResults { get; set; }
    }
}