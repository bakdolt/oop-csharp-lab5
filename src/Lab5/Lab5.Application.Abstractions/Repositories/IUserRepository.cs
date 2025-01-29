using Lab5.Application.Models.UserModels;

namespace Lab5.Application.Abstractions.Repositories;

public interface IUserRepository
{
    User? FindUserByAccountNumber(int accountNumber);

    void UpdateUserMoney(User user);

    void UpdateUserPinCode(User user);

    void AddNewUser(User user);

    void AddOperation(User user, UsersOperation operation);

    IReadOnlyCollection<string> GetOperations(User user);
}
