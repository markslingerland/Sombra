using System;

namespace Sombra.Core
{
    public static class Helpers
    {
        public static string GetUserName(IHasUserName user) => $"{user.FirstName} {user.LastName}";
    }
}