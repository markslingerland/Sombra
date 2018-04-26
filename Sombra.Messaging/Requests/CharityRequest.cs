using System.Collections.Generic;
using Sombra.Messaging.Responses;

namespace Sombra.Messaging.Requests{
    public class CharityRequest : Request<CharityResponse>
    {
        public string CharityId { get; set; }
        public string NameCharity { get; set; }
        public string NameOwner { get; set; }
    }