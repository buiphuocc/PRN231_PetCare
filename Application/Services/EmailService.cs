using Application.IService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var smtp = new SmtpClient
            {
                Host = _config["EmailSettings:SmtpHost"],
                Port = int.Parse(_config["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(_config["EmailSettings:Username"], _config["EmailSettings:Password"]),
                EnableSsl = true,
            };

            var message = new MailMessage(_config["EmailSettings:FromEmail"], toEmail, subject, body)
            {
                IsBodyHtml = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}
