using Npgsql;

namespace Lab5.Infrastructure.DataAccess;

public class DataBaseInitializer
{
    private readonly CurrentConnectionManager connectionManager;
    private readonly string defaultPassword;

    public DataBaseInitializer(CurrentConnectionManager connectionManager, string defaultPassword = "admin")
    {
        this.connectionManager = connectionManager;
        this.defaultPassword = defaultPassword;
    }

    public void InitializeDatabase()
    {
        NpgsqlConnection connection = connectionManager.GetConnection();

        CreateSystemPasswordTable(connection);
        CreateUsersTable(connection);
        CreateOperationsTable(connection);
        InitializeSystemPassword(connection);
    }

    private void CreateSystemPasswordTable(NpgsqlConnection connection)
    {
        string createSystemPasswordTableSql = @"
                CREATE TABLE IF NOT EXISTS SystemPassword (
                    HashedPassword BIGINT NOT NULL PRIMARY KEY
                );";

        using var cmd = new NpgsqlCommand(createSystemPasswordTableSql, connection);
        cmd.ExecuteNonQuery();
    }

    private void CreateUsersTable(NpgsqlConnection connection)
    {
        string createUsersTableSql = @"
                CREATE TABLE IF NOT EXISTS Users (
                    AccountNumber INT PRIMARY KEY,
                    HashedPinCode BIGINT NOT NULL,
                    Balance INT NOT NULL
                );";

        using var cmd = new NpgsqlCommand(createUsersTableSql, connection);
        cmd.ExecuteNonQuery();
    }

    private void CreateOperationsTable(NpgsqlConnection connection)
    {
        string createOperationsTableSql = @"
                CREATE TABLE IF NOT EXISTS Operations (
                    OperationId SERIAL PRIMARY KEY,
                    AccountNumber INT,
                    OperationType TEXT NOT NULL,
                    Amount INT NOT NULL
                );";

        using var cmd = new NpgsqlCommand(createOperationsTableSql, connection);
        cmd.ExecuteNonQuery();
    }

    private void InitializeSystemPassword(NpgsqlConnection connection)
    {
        string checkPasswordSql = "SELECT COUNT(*) FROM SystemPassword;";
        using var checkCmd = new NpgsqlCommand(checkPasswordSql, connection);
        object? result = checkCmd.ExecuteScalar();
        int count = result != DBNull.Value ? Convert.ToInt32(result) : 0;

        if (count == 0)
        {
            string insertPasswordSql = "INSERT INTO SystemPassword (HashedPassword) VALUES (@HashedPassword);";
            using var insertCmd = new NpgsqlCommand(insertPasswordSql, connection);
            insertCmd.Parameters.AddWithValue("HashedPassword", defaultPassword.GetHashCode(StringComparison.Ordinal));
            insertCmd.ExecuteNonQuery();
        }
    }
}
