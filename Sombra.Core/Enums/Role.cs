using System;

namespace Sombra.Core.Enums
{
    [Flags]
    public enum Role
    {
        Default = 0,
        Donator = 1,
        CharityOwner = 2,
        CharityUser = 4,
        EventOrganiser = 8,
        EventParticipant = 16
    }
}