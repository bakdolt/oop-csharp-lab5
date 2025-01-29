using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.UserModels;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CurrentConnectionManager connectionManager;

    public UserRepository(CurrentConnectionManager connectionManager)
    {
        this.connectionManager = connectionManager;
    }

    public void AddNewUser(User user)
    {
        const string insertUserSqlQuery = @"
        INSERT INTO Users (AccountNumber, HashedPinCode, Balance)
        VALUES (@accountNumber, @hashedPinCode, @balance);";

        using var cmd = new NpgsqlCommand(insertUserSqlQuery, connectionManager.GetConnection());

        cmd.Parameters.AddWithValue("accountNumber", user.AccountNumber);
        cmd.Parameters.AddWithValue("hashedPinCode", user.HashedPinCode);
        cmd.Parameters.AddWithValue("balance", user.Balance);

        cmd.ExecuteNonQuery();
    }

    public User? FindUserByAccountNumber(int accountNumber)
    {
        const string findUserSqlQuery = @"
        SELECT AccountNumber, HashedPinCode, Balance
        FROM Users
        WHERE AccountNumber = @accountNumber;";

        using var cmd = new NpgsqlCommand(findUserSqlQuery, connectionManager.GetConnection());

        cmd.Parameters.AddWithValue("accountNumber", accountNumber);

        using NpgsqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            int hashedPassword = reader.GetInt32(1);
            int balance = reader.GetInt32(2);
            return new User(accountNumber, hashedPassword, balance);
        }
        else
        {
            return null;
        }
    }

    public void UpdateUserMoney(User user)
    {
        const string updateUserMoneySqlQuery = @"
        UPDATE Users
        SET Balance = @newBalance
        WHERE AccountNumber = @accountNumber AND HashedPinCode = @hashedPinCode;";

        using var cmd = new NpgsqlCommand(updateUserMoneySqlQuery, connectionManager.GetConnection());

        cmd.Parameters.AddWithValue("newBalance", user.Balance);
        cmd.Parameters.AddWithValue("accountNumber", user.AccountNumber);
        cmd.Parameters.AddWithValue("hashedPinCode", user.HashedPinCode);

        cmd.ExecuteNonQuery();
    }

    public void UpdateUserPinCode(User user)
    {
        const string updateUserMoneySqlQuery = @"
        UPDATE Users
        SET HashedPinCode = @hashedPinCode
        WHERE AccountNumber = @accountNumber;";

        using var cmd = new NpgsqlCommand(updateUserMoneySqlQuery, connectionManager.GetConnection());

        cmd.Parameters.AddWithValue("hashedPinCode", user.HashedPinCode);
        cmd.Parameters.AddWithValue("accountNumber", user.AccountNumber);

        cmd.ExecuteNonQuery();
    }

    public void AddOperation(User user, UsersOperation operation)
    {
        const string addOperationsSqlQuery = @"
        INSERT INTO Operations (AccountNumber, OperationType, Amount)
        VALUES (@accountNumber, @operationType, @amount);";

        using var cmd = new NpgsqlCommand(addOperationsSqlQuery, connectionManager.GetConnection());

        cmd.Parameters.AddWithValue("accountNumber", user.AccountNumber);
        string[] parts = operation.ToString().Split(' ');
        cmd.Parameters.AddWithValue("operationType", parts[0]);
        cmd.Parameters.AddWithValue("amount", int.Parse(parts[1]));

        cmd.ExecuteNonQuery();
    }

    public IReadOnlyCollection<string> GetOperations(User user)
    {
        var result = new List<string>();

        const string getOperationsSqlQuery = @"
        SELECT OperationType, Amount
        FROM Operations
        WHERE AccountNumber = @accountNumber;";

        using var cmd = new NpgsqlCommand(getOperationsSqlQuery, connectionManager.GetConnection());

        cmd.Parameters.AddWithValue("accountNumber", user.AccountNumber);

        using NpgsqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string operationType = reader.GetString(0);
            int amount = reader.GetInt32(1);

            result.Add($"{operationType}: {amount}");
        }

        return result.AsReadOnly();
    }
}
