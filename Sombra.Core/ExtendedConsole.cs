using System;

namespace Sombra.Core
{
    public static class ExtendedConsole
    {
        public static void Log(object message)
        {
            Log(message.ToString());
        }

        public static void Log(string message)
        {
            Console.WriteLine($"{DateTime.UtcNow:MM/dd/yyyy HH:mm:ss} - {message}");
        }
    }
}