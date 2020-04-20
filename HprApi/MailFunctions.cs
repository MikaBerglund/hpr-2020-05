using HrpAbstractions.Mail;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using SendGridServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HprApi
{
    public class MailFunctions
    {
        public MailFunctions(MailService mail)
        {
            this.Mail = mail ?? throw new ArgumentNullException(nameof(mail));
        }

        private MailService Mail;



        [FunctionName(Names.SendEmailWithTemplate)]
        public async Task<HttpResponseMessage> SendEmailWithTemplateAsync([HttpTrigger(AuthorizationLevel.Function, "POST", Route = "Mail/ByTemplate")]HttpRequestMessage request)
        {
            var json = await request.Content.ReadAsStringAsync();
            var mailRequest = JsonConvert.DeserializeObject<SendMailByTemplateRequest>(json);
            await this.Mail.SendEmailWithTemplateAsync(mailRequest);

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}
