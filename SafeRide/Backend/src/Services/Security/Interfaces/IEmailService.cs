using SafeRide.src.Models;

namespace SafeRide.src.Security.Interfaces;

public interface IEmailService
{
    public bool SendOTP(string userAddress, OTP otp);
}