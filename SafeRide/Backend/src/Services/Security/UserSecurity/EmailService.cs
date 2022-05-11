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
            this._server = "smtp.office365.com";
            this._serverPort = 587;
            this._serverAddress = "saferiderzz@outlook.com";
            this._serverPassword = "safeAF_bruh";
        }

        public bool SendOTP(string userAddress, OTP generatedOTP)
        {
            string body = generatedOTP.Passphrase;
            string subject = "SafeRide Login Attempt - Your Temporary One-Time Password for Authentication";
            bool isSuccess;

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
                    smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpServer.EnableSsl = true;

                    try
                    {
                        smtpServer.Send(mail);
                        Console.WriteLine("Email sent successfully.");
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        isSuccess = false;
                    }
                }
            }
            return isSuccess;
        }
    }
}


