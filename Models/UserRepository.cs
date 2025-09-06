
using Microsoft.Data.SqlClient;

public class UserRepository
{
    private readonly string connectionString = "your_connection_string_here";

    public User GetUserByUsername(string username)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var query = "SELECT UserID, Username, Email FROM Users WHERE Username = @username";
        using var command = new SqlCommand(query, connection);
        var safeUsername = InputSanitizer.SanitizeUsername(username);
        command.Parameters.AddWithValue("@username", safeUsername);

        using var reader = command.ExecuteReader();
        if (reader.Read())
            return new User
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Email = reader.GetString(reader.GetOrdinal("Email"))
            };

        return null;
    }
}
