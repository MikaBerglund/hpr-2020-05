using HprAbstractions.Configuration;
using HprAbstractions.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SendGridServices
{
    public class MailService
    {
        public MailService(SendGridConfigSection config)
        {
            this.Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        private SendGridConfigSection Config;


        public async Task SendEmailWithTemplateAsync(SendMailByTemplateRequest request)
        {
            var msg = new SendGridMessage
            {
                Personalizations = new List<Personalization>(),
                TemplateId = request.TemplateId,
                From = new EmailAddress(this.Config.FromAddress, this.Config.FromName)
            };

            msg.Personalizations.Add(new Personalization
            {
                TemplateData = request.Data ?? new Dictionary<string, string>(),
                Tos = new List<EmailAddress>
                {
                    new EmailAddress(request.To)
                }
            });

            var client = new SendGridClient(this.Config.ApiKey);
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                // The e-mail was not sent, so we need to report that with an exception.
                var body = await response.Body.ReadAsStringAsync();
                throw new System.Net.Http.HttpRequestException($"The SendGrid API returned status code: {response.StatusCode}. Detailed response body: {body}");
            }
        }

    }
}
