using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using ProfileAPI.Data;
using System.Net;

namespace ProfileAPI.Services
{
    public class EmailService
    {
        private readonly ProfileContext _context;

        public EmailService(ProfileContext context)
        {
            _context = context;
        }

        public void SendVerificationEmail(string email, string verificationCode)
        {
            // construct the email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Ebrahim", "ebrahim3adel@gmail.com"));
            message.To.Add(new MailboxAddress("Recipient", email));
            message.Subject = "Verification Code";
            message.Body = new TextPart("plain") { Text = $"Your verification code is: {verificationCode}" };

            // Connect to the email server and send the message
            using var client = new SmtpClient();
            client.Connect("smtp-relay.sendinblue.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("ebrahim3adel@gmail.com", "S0XBpv2Yyx4zJjbf");
            client.Send(message);
            client.Disconnect(true);
        }

        public string GenerateVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 9999);
            return code.ToString();
        }

        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            var user = await _context.Info.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            if (user.EmailConfirmCode != code)
            {
                return false;
            }

            user.EmailIsChecked = true;
            user.EmailConfirmCode = null;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
