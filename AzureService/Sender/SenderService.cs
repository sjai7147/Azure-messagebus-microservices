using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.Text;
namespace Sender{
    public class SenderService : ISenderService
    {
        private readonly string _connectionString="Endpoint=sb://jai-service-bus.servicebus.windows.net/;SharedAccessKeyName=sendclaim;SharedAccessKey=7jHeUr64xOGmf6q3sQPMaeWQYBQ+NukIghc4sDp0GP8=";
        private readonly string _queueName="service-bus-queue";
        private IQueueClient _queueClient;
        public SenderService()
        {
            this._queueClient=new QueueClient(this._connectionString,this._queueName);
        }

        public async Task Publish(string mess){
            var message=new Message(Encoding.UTF8.GetBytes(mess));
            await this._queueClient.SendAsync(message);
            await this._queueClient.CloseAsync();
        }
    }
}