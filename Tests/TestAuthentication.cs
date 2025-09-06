
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

[TestFixture]
public class TestAuthentication
{
    private readonly HttpClient client = new HttpClient();

    [Test]
    public async Task TestLoginWithInvalidCredentials()
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", "invaliduser"),
            new KeyValuePair<string, string>("password", "wrongpass")
        });

        var response = await client.PostAsync("http://localhost:5000/login", content);
        Assert.AreEqual(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
