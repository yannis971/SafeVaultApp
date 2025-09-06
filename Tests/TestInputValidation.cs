using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Legacy;

[TestFixture]
public class TestInputValidation
{
    private readonly HttpClient client = new HttpClient();

    [Test]
    public async Task TestForSQLInjection()
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", "admin'; DROP TABLE Users;--"),
            new KeyValuePair<string, string>("email", "attacker@example.com")
        });

        var response = await client.PostAsync("http://localhost:5000/submit", content);
        var responseText = await response.Content.ReadAsStringAsync();

        //Assert.False(responseText.Contains("DROP"), "SQL Injection vulnerability detected");
        StringAssert.DoesNotContain("DROP", responseText, "SQL Injection vulnerability detected");
    }

    [Test]
    public async Task TestForXSS()
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", "<script>alert('xss')</script>"),
            new KeyValuePair<string, string>("email", "xss@example.com")
        });

        var response = await client.PostAsync("http://localhost:5000/submit", content);
        var responseText = await response.Content.ReadAsStringAsync();

        //Assert.False(responseText.Contains("<script>"), "XSS vulnerability detected");
        StringAssert.DoesNotContain("<script>", responseText, "XSS vulnerability detected");
    }
}
