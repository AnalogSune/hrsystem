
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IMailService
    {
        Task SendMessage(string subject, string body, string destination, string name);
    }
}