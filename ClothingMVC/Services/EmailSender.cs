using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services; 

public class EmailSender : IEmailSender   
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var host = _config["EmailSettings:Host"];
        var port = int.Parse(_config["EmailSettings:Port"] ?? "587"); // Default port
        var fromEmail = _config["EmailSettings:Email"];
        var displayName = _config["EmailSettings:DisplayName"] ?? "2FT Clothing";
        var password = _config["EmailSettings:Password"];
        var enableSSL = bool.Parse(_config["EmailSettings:EnableSSL"] ?? "true");

        try
        {
            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = enableSSL
            };

            var mail = new MailMessage
            {
                From = new MailAddress(fromEmail, displayName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mail.To.Add(email);

            await client.SendMailAsync(mail);
        }
        catch (Exception ex)
        {
            // For development: this lets you see why the email failed in your Console
            Console.WriteLine($"Email Failed: {ex.Message}");
        }
    }   
}