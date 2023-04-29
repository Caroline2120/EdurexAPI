using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdurexEnrollment.Core.Interface
{
    public interface IMessagingService
    {
        Task<bool> SendEmail(string email, string message, string subject);
        Task<bool> SendSMS(string phone, string message);
    }
}
