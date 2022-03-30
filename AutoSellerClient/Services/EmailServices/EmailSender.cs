using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Models.AppSettingsOptionsModels;
using Newtonsoft.Json.Linq;
using Services.EmailServices.EmailTemplates;

namespace Services.EmailServices;

public class EmailSender : IMailJetEmailSender
{
    private readonly MailjetOptions _mailjetOptions;

    public EmailSender(IOptions<MailjetOptions> options)
    {
        _mailjetOptions = options.Value;
    }
    public async Task MailJetMailSender(string emailRecipient, string subject, string htmlMessage)
    {
        MailjetClient client = new MailjetClient(_mailjetOptions.ApiKey, _mailjetOptions.SecretKey);

        MailjetRequest request = new MailjetRequest
        {
            Resource = Send.Resource,
        }
            .Property(Send.FromEmail, _mailjetOptions.FromEmail)
            .Property(Send.FromName, _mailjetOptions.FromName)
            .Property(Send.Subject, subject)
            .Property(Send.HtmlPart, htmlMessage)
            .Property(Send.Recipients, new JArray {
                new JObject {
                    {"Email", emailRecipient}
                }
            });
        var response = await client.PostAsync(request);
    }

    public Task<string> CreateHtmlMessageCallbackAnchorValueTemplate(string message, string callback, string anchorValue)
    {
        var response = string.Format(EmailTemplate.MessageCallbackAnchorValue, '{','}',message, callback, anchorValue);
        return Task.FromResult(response);
    }

    public Task<string> CreateIntentSenderContactInformationHtml(string firstName, string lastName, string phone,
        string email)
    {
        var response = string.Format(EmailTemplate.IntentSenderContactInfo, firstName, lastName, phone, email);
    return Task.FromResult(response);
    }
}