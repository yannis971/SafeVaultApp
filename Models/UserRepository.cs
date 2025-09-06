
using MySql.Data.MySqlClient;

public class UserRepository
{
    private readonly string connectionString = "your_connection_string_here";

    public User GetUserByUsername(string username)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "SELECT UserID, Username, Email FROM Users WHERE Username = @username";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@username", username);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new User
            {
                UserID = reader.GetInt32("UserID"),
                Username = reader.GetString("Username"),
                Email = reader.GetString("Email")
            };
        }

        return null;
    }
}
