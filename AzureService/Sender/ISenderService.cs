using System.Threading.Tasks;
namespace Sender{
  public  interface ISenderService{
        Task Publish(string message);
    }
}