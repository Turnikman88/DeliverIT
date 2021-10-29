using DeliverIT.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeliverIT.Services.Contracts
{
    public interface IMailService
    {
        Task SendEmailAsync(MailDTO mailRequest);
    }
}
