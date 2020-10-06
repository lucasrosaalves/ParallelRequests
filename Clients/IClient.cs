using System.Net.Http;
using System.Threading.Tasks;

namespace ParallelRequests
{
    public interface IClient
    {
        Task Handle();
    }
}