using System.Threading.Tasks;

namespace EmailQueue.Services
{
    public interface IBusinessLogic
    {
        Task<string> GetResourceName();
        Task ProcessData(string hostName);
    }
}