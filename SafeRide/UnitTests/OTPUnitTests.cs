using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.WebUtilities;
using Backend.Services;
using Xunit;
using SafeRide.src.Models;
using SafeRide.src.Services;
using Newtonsoft.Json;
using System;
using SafeRide.src.DataAccess;
using Xunit.Abstractions;
using System.Net.Http.Headers;
using Backend.src.Services.Security.UserSecurity;
using SafeRide.src.Security.UserSecurity;

namespace SRUnitTests
{
	public class OTPUnitTests
	{
		private readonly ITestOutputHelper output;

		public OTPUnitTests(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public void TestOTPGeneration()
		{
			OTPService otpService = new OTPService();
			EmailService emailService = new EmailService();
			otpService.GenerateOTP();

			string actual = otpService.GetOTP().Passphrase;
			string expected = "aishjdjshndfjkajkr23";
			output.WriteLine(actual);
			Assert.Equal(expected, actual);

		}

		[Fact]
		public void TestOTPEmail()
		{
			OTPService otpService = new OTPService();
			EmailService emailService = new EmailService();
			otpService.GenerateOTP();
			OTP otp = otpService.GetOTP();
			string testEmail = "colincreasman@gmail.com";

			bool expected = true;
			bool actual = emailService.SendOTP(testEmail, otp);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestOTPValidation()
		{
			OTPService otpService = new OTPService();
			otpService.GenerateOTP();
			OTP otp = otpService.GetOTP();
			string actualOTP = otp.Passphrase;

			bool expected = true;
			bool actual = otpService.ValidateOTP(otp, actualOTP);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public async Task RequestOTP()
		{
			await using var application = new CustomWebAppFactory();
			using var client = application.CreateClient();

			var request = new HttpRequestMessage(HttpMethod.Post, "api/login");

			request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new
			{
				UserName = "colin",
				Email = "colincreasman@gmail.com",
				Role = "Admin",
				Valid = true
			}), Encoding.UTF8, "application/json");

			// act

			using var response = await client.SendAsync(request);

			// assert

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}


		//[Fact]
		//public async Task VerifyOTP()
		//{

		//	//OTPService otpService = new OTPService();
		//	//EmailService emailService = new EmailService();
		//	//otpService.GenerateOTP();
		//	//OTP otp = otpService.GetOTP();
		//	//string otpPass = otp.Passphrase;
		//	//string testEmail = "colincreasman@gmail.com";
		//	//bool success = emailService.SendOTP(testEmail, otp);


		//	await using var application = new CustomWebAppFactory();
		//	using var client = application.CreateClient();

		//	var request1 = new HttpRequestMessage(HttpMethod.Post, "api/login");

		//	request1.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new
		//	{
		//		UserName = "colin",
		//		Email = "colincreasman@gmail.com",
		//		Role = "Admin",
		//		Valid = true
		//	}), Encoding.UTF8, "application/json");

		//	// act

		//	using var response1 = await client.SendAsync(request1);




		//	var queryString = new Dictionary<string, string>()
		//	{
		//		{ "otpPassphrase", otpPass }
		//	};

		//	var requestUri = QueryHelpers.AddQueryString("api/verifyOTP", queryString);

		//	var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

		//	request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new
		//	{
		//		UserName = "colin",
		//		Email = "colincreasman@gmail.com",
		//		Role = "Admin",
		//		Valid = true
		//	}), Encoding.UTF8, "application/json");

		//	using var response = await client.SendAsync(request);

		//	Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		//}
	}
}

