using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;

namespace ASPNETCoreIdentityDemo.Services
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Fake email sent to {email}: {subject}");
            return Task.CompletedTask;
        }
    }
}
