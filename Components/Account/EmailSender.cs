using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using chatapp_blazor.Models;

namespace chatapp_blazor.Components.Account;

public class EmailSender(EmailAuth emailAuth) : IEmailSender<User>
{
    public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(MailboxAddress.Parse(emailAuth.Username));
        mimeMessage.To.Add(MailboxAddress.Parse(email));
        mimeMessage.Subject = "Confirm your email";
        mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = $$"""
                      <div style="width: 100%; font-family: 'DM Sans', sans-serif, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol';">
                          <div style="max-width: 600px; margin: auto">
                              <div style="background-color: #E2E8F0; padding: 2.5rem;">
                                  <div style="background-color: #FFFFFF; padding: 1.25rem;">  
                                      <p style="margin-top: 1.25rem;">Dear {{ user.Name }},</p>
                                      <p style="margin-top: 1.25rem; margin-bottom: 1.25rem;">
                                          Thank you for registering with us. To complete your registration, please confirm your email address by clicking the link below:
                                      </p> 
                                      <a href="{{ confirmationLink }}" style="color: #2563EB;">Confirm my email</a>
                                      <p style="margin-top: 1.25rem;"><b>Note</b>: The link will expire in {{ TimeSpan.FromHours(1).Hours }} hour.</p>
                                  </div>
                              </div>
                          </div>
                      </div>
                      """
        };
        
        var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailAuth.Host, emailAuth.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(emailAuth.Username, emailAuth.Password);
        await smtp.SendAsync(mimeMessage);
        await smtp.DisconnectAsync(true); 
    }

    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }
}