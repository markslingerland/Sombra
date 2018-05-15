using Sombra.Core.Enums;

namespace Sombra.Messaging.Responses{
    public class GetCharityResponse : Response {
        public GetCharityResponse() { }
        public GetCharityResponse(bool success)
        {
            Success = success;

        }
        public bool Success { get; set; }
        public string CharityId { get; set; }
        public string NameOwner { get; set; }
        public string NameCharity { get; set; }
        public string EmailCharity { get; set; }
        public Category Category { get; set; }
        public int KVKNumber { get; set; }
        public string IBAN { get; set; }
    }

    
}

