using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Azure.ServiceBus;

namespace AzureService.Receiver
{
    public class ReceiverService : IReceiverService
    {
         //we will put connection string and quename in configuration
        private  string _connectionString="Endpoint=sb://jai-service-bus.servicebus.windows.net/;SharedAccessKeyName=receiveclaim;SharedAccessKey=uAbFscFp7HMxS7FVpbPuaA7hb6cx1YrG/ckSOwQ1iF0=";
        private  string _queueName="service-bus-queue";
        private readonly IQueueClient _queueClient;
        public ReceiverService()
        {
              this._queueClient=new QueueClient(this._connectionString,this._queueName);
        }

        public void Subscribe(){
            //create messageOption
            var messageHandlerOption=new MessageHandlerOptions( (ex)=>{
                //Console.Write(ex.Message);
                 return Task.CompletedTask;
                }
                ){
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            
            this._queueClient.RegisterMessageHandler(
                async (message,token)=>{
                    //do the work necessary on fetching message from sernder
                    string data=Encoding.UTF8.GetString(message.Body);
                    //process message
                    await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
                },messageHandlerOption);
            
        }
    

    }
}