using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.Security.Interfaces;
using System.Net;  
using System.Net.Mail; 
using System;
using System.Diagnostics;
using System.Threading;

namespace SafeRide.src.Security.UserSecurity;

public class OTPService : IOTPService
{
    //private IUserSecurityDAO _userSecurityDao;
    private string _userEmail;
    private OTP _generatedOTP;
    private int _attempts;
    private Stopwatch _authTimer;
    private bool _isValidated;

    public OTPService()
    {
       // _userEmail = user.Email;
     
        _attempts = 0;
        _authTimer = new Stopwatch();
        //_generatedOTP = new OTP();        
        //_disableAcct = false; 
        _isValidated = false;  
        
        
        //_isValidated = ValidateOTP(_generatedOTP);  
        

        // // continously check OTP to see if it has expired or been used 
        // while (true) {
        //     // if so, create a new OTP
        //     if (_generatedOTP.IsExpired || _generatedOTP.IsUsed) {
        //         _generatedOTP = new OTP();
        //     }
        //     else {
        //         continue;
        //     }
        // }
    }

    public void GenerateOTP()
    {
        _generatedOTP = new OTP();      
    }


    public bool ValidateOTP(string providedOTP)
    {
        // if current otp is expired or has been used, create a new OTP before sending 
        if (_generatedOTP.IsExpired || _generatedOTP.IsUsed)
        {
            _generatedOTP = new OTP();
        }

        // Console.WriteLine("Please enter the OTP sent to your email: ");
        // string providedOTP = Console.ReadLine();

        // only start the 24timer on the first attempt
        if (_attempts == 0)
        {
            _authTimer.Start();
        }

        _attempts += 1; //increment attempts every time Validate is called
        TimeSpan limit = _authTimer.Elapsed;

        if (_attempts > 5 && limit.TotalHours >= 24)
        {
            /*
            _disableAcct = true; 
            //return false;
        */
        }

        if (_generatedOTP.Compare(providedOTP))
        {
            _isValidated = true;
            _generatedOTP.IsUsed = true;  // set IsUsed to true so that the OTP cannot be used again
            //return true;
        }
        return _isValidated;
    }

    public OTP GetOTP()
    {
        return this._generatedOTP;
    }


    // continue calling ValidateOTP until user successfuly completes validation 
    //while (!_isValidated) {
    // stop validating if the user has reached the 24hr limit
    /*if (_disableAcct) {
        Console.WriteLine("User has failed 5 consecutive authentication attempts in the last 24hrs. Account must be disabled");
        // TODO: figure out how to disable the account at this point
        break; 
    }
    else {
        ValidateOTP();
    }*/
    //
}