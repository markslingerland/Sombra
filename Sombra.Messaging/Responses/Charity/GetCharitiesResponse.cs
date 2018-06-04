using System.Collections.Generic;

namespace Sombra.Messaging.Responses.Charity
{
    public class GetCharitiesResponse : Response
    {
        public int TotalNumberOfResults { get; set; }
        public List<Shared.Charity> Results { get; set; }
    }
}