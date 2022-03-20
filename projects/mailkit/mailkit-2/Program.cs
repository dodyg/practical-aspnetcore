using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Pop3;
using MailKit.Security;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Mailkit}/{action=Index}");

app.Run();

public interface IEmailService
{
    List<EmailMessage> ReceiveEmail(int maxCount = 10);
}
public class EmailService : IEmailService
{
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

namespace PracticalAspNetCore.Controllers
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
            return View("/Views/Mailkit/Index.cshtml");
        }

        [Route("Mailkit/Retrieve")]
        public IActionResult Retrieve()
        {
            List<EmailMessage> emails = _emailService.ReceiveEmail(50);
            return RedirectToAction("Index");
        }
    }

}
