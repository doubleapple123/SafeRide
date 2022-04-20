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

namespace SRUnitTests
{
	public class ParseResponseUnitTest
	{
		//[Theory]
		//[InlineData(jsonResponse)]
		public dynamic directionsResponse;

		[Fact]
		public async Task ParseDirectionsResponse()
		{
            HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync("https://api.mapbox.com/directions/v5/mapbox/driving/-73.99045550189928%2C40.73225479503364%3B-73.97962084116241%2C40.73551564560131%3B-73.9886679569091%2C40.72716860725768?alternatives=true&geometries=geojson&language=en&overview=simplified&steps=true&access_token=pk.eyJ1IjoiY29saW5jcmVhc21hbiIsImEiOiJjbDIxbGhnZ2QxMW1pM2Jwamp4YW42M25zIn0.WJD2zPxATbnf2utML0OOCQ");

			response.EnsureSuccessStatusCode();
			string jsonResponse = await response.Content.ReadAsStringAsync();
			
			ParseResponseService? parseResponseService = new ParseResponseService(jsonResponse);

            this.directionsResponse = parseResponseService.GetDirectionsResponse();

			//Console.WriteLine(jsonResponse);

			Assert.IsType<DirectionsResponse>(directionsResponse);

		}
	}
}

