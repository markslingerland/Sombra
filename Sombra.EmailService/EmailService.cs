using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Sombra.Messaging.Events;

namespace Sombra.EmailService
{
    public class EmailService : IConsumeAsync<Email>
    {
        public Task Consume(Email message)
        {
            throw new System.NotImplementedException();
        }
    }
}