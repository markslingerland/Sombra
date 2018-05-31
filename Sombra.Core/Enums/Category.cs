using System;
using System.ComponentModel.DataAnnotations;

namespace Sombra.Core.Enums
{
    [Flags]
    public enum Category
    {
        None = 0,

        [Display(Name = "Kerk en levensbeschouwing")]
        CurchAndLifePhilosophy = 1,

        [Display(Name = "Sport en recreatie")]
        SportAndRecreation = 2,

        [Display(Name = "Internationale hulp")]
        InternationalAid = 4,

        [Display(Name = "Maatschappelijke en sociale doelen")]
        SocialCharities = 8,

        [Display(Name = "Gezondheid")]
        Health = 16,

        [Display(Name = "Milieu en natuurbehoud")]
        EnvironmentAndNatureConservation = 32,

        [Display(Name = "Dierenbescherming")]
        AnimalProtection = 64,

        [Display(Name = "Cultuur")]
        Culture = 128,

        [Display(Name = "Onderwijs en onderzoek")]
        EducationAndResearch = 256
    }
}
