using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;

namespace Sombra.Messaging.Infrastructure
{
    public static class BusExtensions
    {
        public static IBus WaitForConnection(this IBus bus, int retryInMs = 2500)
        {
            while (!bus.IsConnected)
            {
                Thread.Sleep(retryInMs);
            }

            return bus;
        }
    }
}