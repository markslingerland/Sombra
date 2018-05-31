using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sombra.Web.ViewModels.Charity
{
    public class CharityModel
    {
        public string CharityKey { get; set; }
        public string OwnerUserKey { get; set; }
        public string OwnerUserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string[] Categories { get; set; }
        public string KVKNumber { get; set; }
        public string Anbi { get; set; }
        public string IBAN { get; set; }
        public string CoverImage { get; set; }
        public string Slogan { get; set; }
        public string Url { get; set; }
        public string About { get; set; }
    }
}
