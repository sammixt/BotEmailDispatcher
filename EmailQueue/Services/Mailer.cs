//using MailKit.Net.Smtp;
//using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EmailQueue.Services
{
    public class Mailer : IMailer
    {
        IApplicationConfiguration config;
        public Mailer(IApplicationConfiguration _config)
        {
            config = _config;
        }

        string emailTemplatePath = AppDomain.CurrentDomain.BaseDirectory + @"\Template\";
        public async Task<bool> SendEmailAsync(string subject, string content, List<string> emails, List<string> attachmentFiles)
        {
            try
            {
                var template = File.ReadAllText($"{emailTemplatePath}Email.html");
                //template = template.Replace("{name}", name);
                template = template.Replace("{subject}", subject);
                template = template.Replace("{Content}", content);

                string headerImage = $"{emailTemplatePath}ecobankheader.png";
                string footerImage = $"{emailTemplatePath}ecobankfooter.png";

                var message = new MailMessage();
                message.From = new MailAddress($"{config.SmtpUserName}@ecobank.com","ECOBOT");
                foreach (var to in emails)
                {
                    message.To.Add(to);
                }

                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(template, null, MediaTypeNames.Text.Html);
                LinkedResource headerImg = new LinkedResource(headerImage, MediaTypeNames.Image.Jpeg);
                LinkedResource footerImg = new LinkedResource(footerImage, MediaTypeNames.Image.Jpeg);

                headerImg.ContentId = "headerImg";
                footerImg.ContentId = "footerImg";

                avHtml.LinkedResources.Add(headerImg);
                avHtml.LinkedResources.Add(footerImg);

                message.Subject = subject;
                message.Body = template;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(avHtml);

                //var builder = new BodyBuilder();
                //var headlog = builder.LinkedResources.Add(headerImage);
                //headlog.ContentId = "headerImg";
                //var footerlog = builder.LinkedResources.Add(footerImage);
                //footerlog.ContentId = "footerImg";

                //builder.HtmlBody = template;
                if (attachmentFiles.Any())
                {
                    //foreach (string attachment in attachmentFiles)
                    //{
                    //    builder.Attachments.Add(attachment);
                    //}

                    foreach (string attachment in attachmentFiles)
                    {
                        Attachment data = new Attachment(attachment, MediaTypeNames.Application.Octet);
                        ContentDisposition dis = data.ContentDisposition;
                        dis.CreationDate = File.GetCreationTime(attachment);
                        dis.ModificationDate = File.GetLastWriteTime(attachment);
                        dis.ReadDate = File.GetLastAccessTime(attachment);
                        message.Attachments.Add(data);
                    }

                }

                int tries = 1;
                Label_1:
                try
                {
                    await Send(message, tries);
                    return true;
                }
                catch (Exception ex)
                {
                    tries += 1;
                    if (tries > 3)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                    goto Label_1;
                }
                

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task Send(MailMessage message, int tryCount)
        {
            using (var client = new SmtpClient())
            {
                //client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                string server = null;
                
                switch (tryCount)
                {
                    case 1:
                        server = config.SmtpServerOne;
                        break;
                    case 2:
                        server = config.SmtpServerTwo;
                        break;
                    case 3:
                        server = config.SmtpServerThree;
                        break;
                    default:
                        server = config.SmtpServerOne;
                        break;

                }

                client.Host = server;
                client.Port = config.Port;
                Console.WriteLine($"Connectint to Mail Server {tryCount}");
                client.Credentials = new NetworkCredential(config.SmtpUserName, config.SmtpPassword);
                Console.WriteLine($"Sending Mail");
                client.Send(message);
                //await client.ConnectAsync(server,,false);


                //await client.AuthenticateAsync(config.SmtpUserName, config.SmtpPassword);

                //await client.SendAsync(message);
                //await client.DisconnectAsync(true);
            }
        }
    }
}
