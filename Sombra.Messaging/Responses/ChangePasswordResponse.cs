namespace Sombra.Messaging.Responses
{
    public class ChangePasswordResponse : Response
    {
        public  ChangePasswordResponse(){
            
        }
        public ChangePasswordResponse(bool success)
        {
            Success = success;

        }
        public bool Success { get; set; }
    }
}