
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EnableBankingTest.SwaggerResponses;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

public class EndpointTests
{
    private HttpClient _client;

    [Fact]
    public async Task ValidToken_ReturnsOk()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

        // Arrange
        var token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsIng1dSI6Imh0dHBzOi8vZG1jaWNvbnNkZXZlbG9wbWVudC5kZWxkaW9zbW90b3JjbHViYWRtaW4uY29tL0ljb25zL2VuY" +
                    "WJsZWJhbmtpbmd0ZXN0LWNlcnQtcHVibGljLnBlbSJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ik0gVGFsaGEgQXJzaGFkIiwiaWF0IjoxNTc4NzcwMDAwLCJleHAiOjE3Nz" +
                    "g3NTIwMDB9.dzjDBpeTKqW7FGYj9Utz0m_1iak6TkgyFEGL1j6AfpaWYlc46d_ROUhtnp5TsZc5XPBaW1fGoZ5vrG5jf080bJm0MUhBMsMoUHmUnU5PFhkpyZhY1ZXxl6ic-wMW" +
                    "eKU4o4OBgDroQS8eme1bht6MmodZrMWTWyfevw_rprzvz1Yv7qvZP7yElaXOFBNdpODP3vLMve_Pq9HTkfk3VBpW-My8wuOEIy9ZbrXD84Yhib72pFNHa7p_m_fgP947qprh-Tg" +
                    "eZ9PViU3LHYCPlrumMUv4U69wPTRVpgoyq5hnqlDbPM-FaxhR7Jg_FQ9kKofHrj6dWWMWOZIbeCgIoS0uIg";

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/JwtValidation");
        request.Headers.Add("Authorization", "Bearer " + token);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ExpiredToken_ReturnsBadRequest()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");
        // Arrange
        var expiredToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsIng1dSI6Imh0dHBzOi8vZG1jaWNvbnNkZXZlbG9wbWVudC5kZWxkaW9zbW90b3JjbHViYWRtaW4uY29tL0ljb25zL2VuYWJsZWJhb" +
                           "mtpbmd0ZXN0LWNlcnQtcHVibGljLnBlbSJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ik0gVGFsaGEgQXJzaGFkIiwiaWF0IjoxMjc4NzcwMDAwLCJleHAiOjEyNzg3NTIwMDB9.ThtD6C-" +
                           "xq6SbZV9WdgdsCNmsLtfnm2xxJgVrOxPQKNjhTQ3sLhBRX-NWzO4T4_7Ttg0Hm1sSZhu-IoUDqbuFR-1ijeytr2C4KIe_" +
                           "ZsSKkX5T27_AafXZgNVQ2oU6HD4ynGLMyiTwzvnWyIVcHfVmcL0sI2MNhyt4fxtCirv-pQhB6jLVjgAMcOr3IFsCeMkmmPBUBna2h4Q8n0rXIUrZdP0a71rkc-IoSORCgu1ex68xV5yPVl6hne9fAc6t6yoYjJMfbvzpo79Bq__qeQ" +
                           "Y_6hi7zL44cL34Fz_5pc1HTuQxYo7wxZU548l8x5WnreIVdQGd9rreMgDeLOIor7fvgw";
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/JwtValidation");
        request.Headers.Add("Authorization", "Bearer " + expiredToken);

        // Act
        var response = await _client.SendAsync(request);

        // Assert

        string message = "";
        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseBody);
                
            message = errorResponse.Message;

        }

        Assert.Equal(message, "Token has expired");
    }

    [Fact]
    public async Task InvalidToken_ReturnsBadRequest()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

        // Arrange
        var invalidToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsIng1dSI6Imh0dHBzOi8vZG1jaWNvbnN" +
                           "kZXZlbG9wbWVudC5kZWxkaW9zbW90b3JjbHViYWRtaW4uY29tL0ljb25zL2VuYWJsZWJhbmtpbmd" +
                           "0ZXN0LWNlcnQtcHVibGljLnBlbSJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ik0gVGFsaGEgQXJzaGFkIiwiaWF0Ijo" +
                           "xNTc4NzcwMDAwLCJleHdwhyuiNzg3NTIwMDB9.dzjDBpeTKqW7FGYj9Utz0m_1iak6TkgyFEGL1j6AfpaWYlc46d_ROUhtnp5TsZc5XPBaW1fGoZ5vrG5jf080bJm0MUhBMsMoUHmUnU5PFhkpyZhY1ZXxl6ic-wMWeKU4o4OBg" +
                           "DroQS8eme1bht6MmodZrMWTWyfevw_rprzvz1Yv7qvZP7yElaXOFBNdpODP3vLMve_Pq9HTkfk3VB" +
                           "pW-My8wuOEIy9ZbrXD84Yhib72pFNHa7p_m_fgP947qprh-TgeZ9PViU3LHYCPlrumMUv4U69wPTRVpgoyq5hnqlDbPM-FaxhR" +
                           "7Jg_FQ9kKofHrj6dWWMWOZIbeCgIoS0uIg";
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/JwtValidation");
        request.Headers.Add("Authorization", "Bearer " + invalidToken);

        // Act
        var response = await _client.SendAsync(request);


        // Assert
        string message = "";
        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseBody);

            message = errorResponse.Message;

        }

        Assert.Equal(message, "Token is invalid");
    }

}
