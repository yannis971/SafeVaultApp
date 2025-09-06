
using System.Text.RegularExpressions;
using System.Web;

public static class InputSanitizer
{
    public static string SanitizeUsername(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var sanitized = Regex.Replace(input, "[^a-zA-Z0-9]", "");
        return sanitized.Length > 20 ? sanitized.Substring(0, 20) : sanitized;
    }

    public static string SanitizeEmail(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var encoded = HttpUtility.HtmlEncode(input);
        var emailPattern = "^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(encoded, emailPattern) ? encoded : string.Empty;
    }
}
