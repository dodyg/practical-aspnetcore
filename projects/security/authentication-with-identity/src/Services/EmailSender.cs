using System.Threading.Tasks;

namespace WebApplication.Services
{
    public sealed class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
			// You need to implement the code to send emails here.
            return Task.CompletedTask;
        }
    }
}