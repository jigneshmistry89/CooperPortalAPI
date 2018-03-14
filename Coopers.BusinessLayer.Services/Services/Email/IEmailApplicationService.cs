using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface IEmailApplicationService
    {

        void SendAnEmail(string Template, string To, string Subject);
    }
}
