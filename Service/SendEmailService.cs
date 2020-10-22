using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using NetCoreRestApiSendEmailWithTemplate.Interface;
using MimeKit;
using System;
using MailKit.Net.Smtp;

namespace NetCoreRestApiSendEmailWithTemplate.Service
{
    public class SendEmailService : ISendEmail
    {
        private readonly IWebHostEnvironment iWebHostEnvironment;

        public SendEmailService(IWebHostEnvironment iWebHostEnvironment)
        {
            this.iWebHostEnvironment = iWebHostEnvironment;
        }

        public async Task sendEmail(string to,string toAdressTitle, string subject, string title, string fromAdressTitle)
        {
            string pathTemplate = iWebHostEnvironment.WebRootPath +
            Path.DirectorySeparatorChar.ToString() +
            "EmailTemplates" +
            Path.DirectorySeparatorChar.ToString() +
            "emailTemplate1.html";
            Console.WriteLine(pathTemplate);

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathTemplate))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            string messageBody = builder.HtmlBody;

            string SmtpServer = "smtp.gmail.com";

            int SmtpPortNumber = 587;

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress
                                        (fromAdressTitle,
                                         "samuelinfo7@gmail.com"
                                         ));
            mimeMessage.To.Add(new MailboxAddress
            (toAdressTitle,
            to
            ));

            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("html")
            {
                Text = messageBody
            };

            using (var client = new SmtpClient())
            {
                client.Connect(SmtpServer, SmtpPortNumber, false);
                client.Authenticate("your email", "your password");
                await client.SendAsync(mimeMessage);
                Console.WriteLine("The mail has been sent successfully !!");
                Console.ReadLine();
                await client.DisconnectAsync(true);
            }



        }
    }
}