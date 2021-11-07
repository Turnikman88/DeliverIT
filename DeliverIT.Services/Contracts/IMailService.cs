using DeliverIT.Services.DTOs;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IMailService
    {
        Task SendEmailAsync(MailDTO mailRequest);
    }
}
