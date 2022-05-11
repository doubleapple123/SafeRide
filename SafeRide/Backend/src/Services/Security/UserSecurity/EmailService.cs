using System;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.Security.Interfaces;
using System.Net;
using System.Net.Mail;
using System;
using System.Diagnostics;
using System.Threading;

namespace Backend.src.Services.Security.UserSecurity
{
	public class EmailService : IEmailService
	{
		private string _server;
		private int _serverPort;
		private string _serverAddress;
		private string _serverPassword;

		public EmailService()
		{
			this._server = "smtp.gmail.com";
			this._serverPort = 587;
			this._serverAddress = "safe.riderzz@gmail.com";
			this._serverPassword = "safeAF_bruh";
		}

        public void SendOTP(string userAddress, OTP generatedOTP)
        {
            string body = generatedOTP.Passphrase;
            string subject = "Attempted SafeRide Login - Your Temporary One-Time Password for Authentication";


            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_serverAddress);
                mail.To.Add(userAddress);
                mail.Subject = subject;
                mail.Body = @$"
                    <html>
                        <body>
                            <p></p>Hello,</p>
                            <p>Your one-time password for authentication is: {body}</p>
                            <p>This is a temporary password that will expire 2 minutes after this email was sent.</p><br>
                            <p>Sincerely,<br><br>
                            SafeRide Security</p>
                        </body>
                    </html>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtpServer = new SmtpClient(_server, _serverPort))
                {
                    smtpServer.UseDefaultCredentials = false;
                    smtpServer.Credentials = new System.Net.NetworkCredential(_serverAddress, _serverPassword);
                    smtpServer.EnableSsl = true;
                    smtpServer.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
            }
        }
    }
}

