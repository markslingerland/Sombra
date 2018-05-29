using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sombra.Web.ViewModels.Charity
{
    public class CharityActionModel
{
    public string CharityActionKey { get; set; }
    public string CharityKey { get; set; }
    public string[] UserKeys { get; set; }
    public string CharityName { get; set; }
    public string[] Category { get; set; }
    public string IBAN { get; set; }
    public string Name { get; set; }
    public string ActionType { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}
}