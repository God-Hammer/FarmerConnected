using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Service.Interfaces;
using Utility.Settings;

namespace Service.Implementations
{
    public class SendMailService : ISendMailService
    {
        private readonly string _sendGridApiKey;
        private readonly string _emailAddress;
        private readonly string _nameApp;
        public SendMailService(IOptions<AppSetting> appSettings)
        {
            _sendGridApiKey = appSettings.Value.SendGridApiKey;
            _emailAddress = appSettings.Value.EMailAddress;
            _nameApp = appSettings.Value.NameApp;

        }

        public async Task SendVerificationEmail(string userEmail, string token)
        {
            var verificationLink = $"https://localhost:7153/api/customers/verify/{token}";

            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress(_emailAddress, _nameApp);
            var to = new EmailAddress(userEmail);
            var subject = "Email Verification";
            var plainTextContent = $"Please verify your email by clicking the following link: {verificationLink}";
            var htmlContent = $"<p>Please verify your email by clicking the following link: <a href=\"{verificationLink}\">{verificationLink}</a></p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
