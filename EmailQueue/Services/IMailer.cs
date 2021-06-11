using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailQueue.Services
{
    public interface IMailer
    {
        Task<bool> SendEmailAsync(string subject, string content, List<string> emails, List<string> attachmentFiles);
    }
}