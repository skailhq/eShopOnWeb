using System;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net.Sockets;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.eShopWeb.Infrastructure.Services;

// This class is used by the application to send email for account confirmation and password reset.
// For more details see https://go.microsoft.com/fwlink/?LinkID=532713
public class EmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        // TODO: Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
        _logger.LogInformation(message);
        
        // Used on demonstrations (using command "nc -l 2525 -v" to show message)
        var addr = Environment.GetEnvironmentVariable("SMTP_SERVER");
        try
        {
            var _addr = addr?.Split(":");
            var server = _addr.IsNullOrEmpty() ? "127.0.0.1" : _addr?[0] ?? "127.0.0.1";
            var _port = _addr.IsNullOrEmpty() || _addr?.Length < 2 ? "127.0.0.1" : _addr?[1];

            var result = Int32.TryParse(_port, out var port);
            TcpClient client = new TcpClient(server, result ? port : 2525);

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            NetworkStream stream = client.GetStream();

            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", message);

            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            _logger.LogInformation(e.Message);
        }
        
        return Task.CompletedTask;
    }
}
