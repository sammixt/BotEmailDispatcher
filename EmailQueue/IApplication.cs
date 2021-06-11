using System.Threading.Tasks;

namespace EmailQueue
{
    public interface IApplication
    {
         Task Run();
    }
}