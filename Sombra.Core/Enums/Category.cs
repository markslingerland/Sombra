using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.Core.Enums
{
    [Flags]
    public enum Category
    {
        //TODO change to english
        None = 0,
        KerkEnLevensbeschouwing = 1,
        SportEnRecreatie = 2,
        InternationaleHulp = 3,
        MaatschappelijkeEnSocialeDoelen = 4,
        Gezondheid = 5,
        MilieuEnNatuurbehoud = 6,
        Dierenbescherming = 7,
        Cultuur = 8,
        OnderwijsEnOnderzoek = 9,
        Overig = 10
    }
}
