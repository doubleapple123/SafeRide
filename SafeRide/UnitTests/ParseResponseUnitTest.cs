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
		//public dynamic directionsResponse;

		// check if the deserializaion of the JSON response from MapBox API on the frontend can be correctly deserialized into the necessary model instances
		[Fact]
		public async Task ParseDirectionsResponse()
		{
            HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync("https://api.mapbox.com/directions/v5/mapbox/driving/-73.99045550189928%2C40.73225479503364%3B-73.97962084116241%2C40.73551564560131%3B-73.9886679569091%2C40.72716860725768?alternatives=true&geometries=geojson&language=en&overview=simplified&steps=true&access_token=pk.eyJ1IjoiY29saW5jcmVhc21hbiIsImEiOiJjbDIxbGhnZ2QxMW1pM2Jwamp4YW42M25zIn0.WJD2zPxATbnf2utML0OOCQ");

			response.EnsureSuccessStatusCode();
			string jsonResponse = await response.Content.ReadAsStringAsync();
			
			ParseResponseService? ParseResponseService = new ParseResponseService(jsonResponse);

            var directionsResponse = ParseResponseService.GetDirectionsResponse();

			Assert.IsType<DirectionsResponse>(directionsResponse);
		}

		// check extracting the step coordinates of a single route from the deserialized response
		[Fact]
		public async Task ParseStepCoordinates()
        {
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync("https://api.mapbox.com/directions/v5/mapbox/driving/-73.99045550189928%2C40.73225479503364%3B-73.9886679569091%2C40.72716860725768?alternatives=true&geometries=geojson&language=en&overview=simplified&steps=true&access_token=pk.eyJ1IjoiY29saW5jcmVhc21hbiIsImEiOiJjbDIxbGhnZ2QxMW1pM2Jwamp4YW42M25zIn0.WJD2zPxATbnf2utML0OOCQ");

			response.EnsureSuccessStatusCode();
			string jsonResponse = await response.Content.ReadAsStringAsync();

			ParseResponseService? ParseResponseService = new ParseResponseService(jsonResponse);

			var directionsResponse = ParseResponseService.GetDirectionsResponse();
			// coords of each step.maneuver in the response from the request URL hard coded above
			var expected = new Dictionary<double, double>()
			{
				[-73.990147] = 40.732215,
				[-73.990027] = 40.732756,
				[-73.985909] = 40.731019,
				[-73.9887] = 40.727182
			};

			var actual = ParseResponseService.GetStepCoordinates();
			

			Assert.Equal(expected, actual);
		}
	}
}

