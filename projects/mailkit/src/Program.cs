using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MailKit.Net.Smtp;
using MailKit.Net.Pop3;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;


namespace MailkitBasic
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IEmailService, EmailService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Mailkit}/{action=Index}");
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

namespace MailkitBasic.Controllers
{
public class MailkitController : Controller
    {
        private readonly IEmailService _emailService;
        public MailkitController(IEmailService EmailService)
        {
            _emailService = EmailService;
        }
        
        public IActionResult Index()
        {
            return View("/src/Views/Mailkit/Index.cshtml");
        }
        [Route("Mailkit/Send")]
        public IActionResult Send()
        {
            _emailService.Send(new EmailMessage()
            {
                SenderName = "Name",
                SenderEmail = "Email",
                RecipientName = "Rec. name",
                RecipientEmail = "Rec. email",
                Subject = "Subject",
                Content = "Good day"
            });
            return RedirectToAction("Index");
        }
    }

}
