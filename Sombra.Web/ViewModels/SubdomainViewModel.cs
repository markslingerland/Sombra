namespace Sombra.Web.ViewModels
{
    public class SubdomainViewModel
    {
        public const string SUBDOMAIN_PARAMETER = nameof(Subdomain);
        public string Subdomain { get; set; }
        public bool HasSubdomain { get; set; }
    }
}