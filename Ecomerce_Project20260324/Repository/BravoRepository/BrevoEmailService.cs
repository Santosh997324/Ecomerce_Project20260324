using Ecomerce_Project20260324.Models.Bravo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Runtime;

namespace Ecomerce_Project20260324.Repository
{
    public class BrevoEmailService : IBrevoEmailService
    {

        private readonly BrevoSettings _settings;

        public BrevoEmailService(IOptions<BrevoSettings> options)
        {
            _settings = options.Value;
        }

        public async System.Threading.Tasks.Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            try
            {

                var config = sib_api_v3_sdk.Client.Configuration.Default;
                config.ApiKey["api-key"] = _settings.ApiKey;


                var apiInstance = new TransactionalEmailsApi();

                var email = new SendSmtpEmail
                {
                    Sender = new SendSmtpEmailSender(_settings.SenderName, _settings.SenderEmail),
                    To = new List<SendSmtpEmailTo>
                            {
            new SendSmtpEmailTo(toEmail)
                             },
                    Subject = subject,
                    HtmlContent = htmlContent
                };

                var result = await apiInstance.SendTransacEmailAsync(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR: " + ex.Message);
                throw;
            }
        }

    }
}
