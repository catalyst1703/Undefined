using Blogs.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;

namespace Blogs.Services;

public class EmailSender : IEmailSender
{
    private readonly IOptions<EmailSettings> _options;

    public EmailSender(IOptions<EmailSettings> options)
    {
        _options= options;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        SmtpClient smtpClient = new SmtpClient(_options.Value.Host, _options.Value.Port)
        {
            Credentials = new  NetworkCredential(_options.Value.SenderEmail,_options.Value.SenderPassword),
            EnableSsl  = _options.Value.EnableSsl
        };
        MailMessage message = new MailMessage();
        message.From = new MailAddress(_options.Value.SenderEmail);
        message.To.Add(email);
        message.Subject = subject;
        message.Body = htmlMessage;
        message.IsBodyHtml = true;

        await smtpClient.SendMailAsync(message);
    }
}