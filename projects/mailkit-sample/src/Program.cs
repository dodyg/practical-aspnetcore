using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MailKit.Net.Smtp;
using MailKit.Net.Pop3;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;


namespace mailkit_sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello Mailkit");
            });
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
    public interface IEmailService
    {
        void Send(EmailMessage email);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
    public class EmailService : IEmailService
    {
        public void Send(EmailMessage email)
        {
            var message = new MimeMessage();
            message.From.Add (new MailboxAddress (email.SenderName, email.SenderEmail));
			message.To.Add (new MailboxAddress (email.RecipientName, email.RecipientEmail));
			message.Subject = email.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = email.Content
            };
            using (var emailClient = new SmtpClient())
            {
                // 587 is for smtp server port, this might change from one server to another.
                // SecureSocketOptions.Auto for SSL.
                emailClient.Connect("smtp server", 587, SecureSocketOptions.Auto);

                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate("smtp server username", "smtp server password");
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
        }
        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            using (var emailClient = new Pop3Client())
            {
                // 995 is for POP3 server port, this might change from one server to another.
                // SecureSocketOptions.Auto for SSL.
                emailClient.Connect("POP3 server name",  995, SecureSocketOptions.Auto);

                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate("POP3 server username", "POP3 server password");
                List<EmailMessage> emails = new List<EmailMessage>();
                for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    // i variable is for maximum number of messages to retrieve
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject,
                        SenderName = message.Sender.Name,
                        SenderEmail = message.Sender.Address
                    };
                }

                return emails;
            }
        }
    }
    public class EmailMessage
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
