using System;
using System.Collections.Generic;
using System.Text;

namespace Sombra.Core.Enums
{
    [Flags]
    public enum Category
    {
        //TODO: change to english
        None = 0,
        KerkEnLevensbeschouwing = 1,
        SportEnRecreatie = 2,
        InternationaleHulp = 4,
        MaatschappelijkeEnSocialeDoelen = 8,
        Gezondheid = 16,
        MilieuEnNatuurbehoud = 32,
        Dierenbescherming = 64,
        Cultuur = 128,
        OnderwijsEnOnderzoek = 256,
        Overig = 512
    }
}
