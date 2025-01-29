using Npgsql;

namespace Lab5.Infrastructure.DataAccess;

public class CurrentConnectionManager : IDisposable
{
    private string? connectionString;

    private NpgsqlConnection? connection;

    public void SetConnectionString(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public NpgsqlConnection GetConnection()
    {
        if (connection == null)
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        return connection;
    }

    public void CloseConnection()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            connection.Dispose();
            connection = null;
        }
    }

    public void Dispose()
    {
        CloseConnection();
    }
}
