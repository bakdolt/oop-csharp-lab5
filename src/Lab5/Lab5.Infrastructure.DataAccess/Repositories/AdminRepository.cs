using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.AdminModels;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly CurrentConnectionManager connectionManager;

    public AdminRepository(CurrentConnectionManager connectionManager)
    {
        this.connectionManager = connectionManager;
    }

    public long GetSystemPasswordHashCode()
    {
        const string getPasswordSql = "SELECT HashedPassword FROM SystemPassword LIMIT 1;";

        using var cmd = new NpgsqlCommand(getPasswordSql, connectionManager.GetConnection());

        object? result = cmd.ExecuteScalar();

        if (result == null || result == DBNull.Value)
        {
            throw new Exception("System password not found.");
        }

        return Convert.ToInt32(result);
    }

    public void UpdateSystemPassword(Admin admin)
    {
        const string updatePasswordSql = "UPDATE SystemPassword SET HashedPassword = @hashedPassword";

        using var cmd = new NpgsqlCommand(updatePasswordSql, connectionManager.GetConnection());

        cmd.Parameters.AddWithValue("hashedPassword", admin.HashedPassword);

        cmd.ExecuteNonQuery();
    }
}
