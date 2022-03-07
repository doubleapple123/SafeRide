﻿using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.Security.Interfaces;
using System.Net;  
using System.Net.Mail; 

namespace SafeRide.src.Security.UserSecurity;

public class OTPService : IOTPService
{
    private IUserSecurityDAO _userSecurityDao;
    private string _userEmail;
    private OTP _generatedOTP;
    
    public OTPService(string email) {
        //_userSecurityDao userSecurityDao= ;
        _userEmail = email;
        _generatedOTP = new OTP();

    //     // testing 2min expiration
    //   //  Thread.Sleep(12001);
    //     ValidateOTP(pass);
    
        //Console.WriteLine("here");
        // continously check OTP to see if it has expired or been used 
        // while (true) {
        //     // if so, create a new OTP
        //     if (_generatedOTP.IsExpired || _generatedOTP.IsUsed) {
        //         _generatedOTP = new OTP();
        //     }
        // }
    }

    public void SendEmail() {
        
        string server = "smtp.gmail.com";
        int servPort = 587;
        string serverAddress = "safe.riderzz@gmail.com";
        string serverPass = "safeAF_bruh";
        string userAddress = _userEmail;
        string subject = "SafeRide - Your Temporary One-Time Password for Authentication";
        string body = _generatedOTP.Passphrase;

        using (MailMessage mail = new MailMessage()) {
            mail.From = new MailAddress(serverAddress);
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
            using (SmtpClient smtpServer = new SmtpClient(server, servPort)) {
                smtpServer.UseDefaultCredentials = false;
                smtpServer.Credentials = new System.Net.NetworkCredential(serverAddress, serverPass);
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
        }
    }

    public bool ValidateOTP(string providedOTP)
    {

        // return _generatedOTP.Compare(providedOTP);
        if (_generatedOTP.Compare(providedOTP) && !_generatedOTP.IsExpired && !_generatedOTP.IsUsed) {
            return true;
        }
        else {
            return false;
        }
    }
}