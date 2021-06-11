using EmailQueue.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailQueue.Services
{
    public class BusinessLogic : IBusinessLogic
    {
        //Get Pending Item
        //Set Status to Processing
        //Update Status To Sent if Successfull or Retry (if Failed and Update Exception Table)
        //for retry, if difference between date logged and current date is greated than 24hrs, flag as failed
        IDataOperation dataOperation;
        IMailer mailer;
        public BusinessLogic(IDataOperation _dataOperation, IMailer _mailer)
        {
            dataOperation = _dataOperation;
            mailer = _mailer;
        }

        public async Task<string> GetResourceName()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            string HostIp = Dns.GetHostByName(hostName).AddressList[0].ToString();
            //try
            //{
            //    var host = await Dns.GetHostEntryAsync(hostName);
            //    hostName = host.AddressList[0].ToString();
            //}
            //catch (Exception)
            //{
            //    // Ignore
            //}
            return HostIp;
        }

        public async Task ProcessData(string hostName)
        {
            var getPendingEmail = dataOperation.GetPendingMessage(hostName);
            if (getPendingEmail.Any())
            {
                List<string> att = new List<string>();
                //var pendingMail = getPendingEmail.FirstOrDefault();
                foreach(var pendingMail in getPendingEmail)
                {
                    Console.WriteLine($"Treating {pendingMail.Subject}");
                    pendingMail.Status = "RUNNING";
                    dataOperation.UpdateStatus(pendingMail);
                    var attachments = dataOperation.GetAttachments(pendingMail.MailID);
                    if (attachments.Any())
                    {
                        Console.WriteLine($"Attaching Files for {pendingMail.Subject}");
                        foreach (var attachment in attachments)
                        {
                            att.Add(attachment.AttachmentPath);
                        }
                    }
                    List<string> emails = pendingMail.Receipient.Split(',').ToList();
                    await mailer.SendEmailAsync(pendingMail.Subject, pendingMail.Body, emails, att)
                        .ContinueWith((x) =>
                        {
                            if (x.Status == TaskStatus.Faulted)
                            {
                                //var hrPast = DateTime.Now - pendingMail.QueueTime.Value;
                                //decimal hoursPast = Convert.ToDecimal(hrPast.TotalHours);
                                if (pendingMail.TryCount >= 20)
                                {
                                    pendingMail.Status = "FAILED";
                                    pendingMail.Error = x.Exception.InnerException.ToString();
                                    pendingMail.ProcessedTime = DateTime.Now;
                                    pendingMail.TryCount += 1; 
                                }
                                else
                                {
                                    pendingMail.Status = "RETRY";
                                    pendingMail.ProcessedTime = DateTime.Now;
                                    pendingMail.TryCount += 1;
                                }
                                
                            }
                            else if (x.Status == TaskStatus.RanToCompletion && x.Result == true)
                            {
                                pendingMail.ProcessedTime = DateTime.Now;
                                pendingMail.Status = "SENT";
                                pendingMail.TryCount += 1;
                                Console.WriteLine($"Email Sent on {hostName} @ {DateTime.Now}");
                            }
                            dataOperation.UpdateStatus(pendingMail);


                        });
                }
               
            }
        }
    }
}
