using System.Collections.Generic;

namespace EmailQueue.Model
{
    public interface IDataOperation
    {
        List<EmailAttachment> GetAttachments(string mailId);
        List<EmailMessageQueue> GetPendingMessage(string hostName);
        void UpdateStatus(EmailMessageQueue model);
    }
}