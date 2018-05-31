namespace Sombra.Messaging.Responses.Template{
    public class EmailTemplateResponse : Response {
        public string Template { get; set; }
        public bool HasTemplate => !string.IsNullOrEmpty(Template);
    }
}