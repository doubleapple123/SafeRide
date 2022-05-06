using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Xunit;

namespace SRUnitTests;

public class RouteSaveTest
{
    private string Test_Acc_Token =
        "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluIiwiQWN0aXZlIjoibm9uLWFjdGl2ZSIsImlzcyI6Ind3dy5zYWZlcmlkZS5uZXQifQ.2xYiGq5peXtvgbdAdSxweTZJxo5sqxBBEMCrDSkQnTQ";
    [Fact]
    public async Task GET_Route_Get()
    {
        //arrange
        await using var application = new CustomWebAppFactory();
        using var client = application.CreateClient();
        
        var queryString = new Dictionary<string, string>()
        {
            { "routeName", "test1" }
        };
        var requestUri = QueryHelpers.AddQueryString("api/route/get", queryString);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Authorization = AuthenticationHeaderValue.Parse(Test_Acc_Token);
        //act

        using var response = await client.SendAsync(request);
        
        //assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Fact]
    public async Task GET_Route_All()
    {
        //arrange
        await using var application = new CustomWebAppFactory();
        using var client = application.CreateClient();
        
        var request = new HttpRequestMessage(HttpMethod.Get, "api/route/all");
        request.Headers.Authorization = AuthenticationHeaderValue.Parse(Test_Acc_Token);
        //act

        using var response = await client.SendAsync(request);
        
        //assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(@"{'routeList':['test1']}", response.Content.ReadAsStringAsync().Result.Replace("\"", "'"));
    }
}