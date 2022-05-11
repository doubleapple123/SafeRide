using SafeRide.src.Models;

namespace SafeRide.src.Security.Interfaces;

public interface IEmailService
{
    public void SendOTP(string userAddress, OTP otp);
}