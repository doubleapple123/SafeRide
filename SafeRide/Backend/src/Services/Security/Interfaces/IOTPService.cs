using SafeRide.src.Models;

namespace SafeRide.src.Security.Interfaces;

public interface IOTPService
{
    public bool ValidateOTP(OTP generatedOTP, string providedOTP);
    public void GenerateOTP();
    public OTP GetOTP();
    //public void SetUser(UserSecurityModel user);
}