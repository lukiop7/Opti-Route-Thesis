using OptiRoute.Application.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
