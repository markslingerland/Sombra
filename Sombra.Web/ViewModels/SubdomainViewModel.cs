namespace Sombra.Web.ViewModels
{
    public abstract class SubdomainViewModel
    {
        public const string SUBDOMAIN_PARAMETER = nameof(Subdomain);
        public string Subdomain { get; set; }
    }
}