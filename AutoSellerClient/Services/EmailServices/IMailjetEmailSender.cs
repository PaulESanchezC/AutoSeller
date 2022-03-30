using Mailjet.Client;

namespace Services.EmailServices;

public interface IMailJetEmailSender
{
    Task MailJetMailSender(string emailRecipient, string subject, string htmlMessage);

    Task<string> CreateHtmlMessageCallbackAnchorValueTemplate(string message, string callback, string anchorValue);

    Task<string> CreateIntentSenderContactInformationHtml(string firstName, string lastName, string phone, string email);
}