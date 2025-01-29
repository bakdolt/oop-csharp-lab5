namespace Lab5.Application.Contracts.UserContracts;

public interface IUserService
{
    LoginResult Login(int accountNumber, int pinCode);

    RegisterResult Register(int accountNumber, int pinCode);

    void Deposit(int amount);

    WithdrawResult Withdraw(int amount);

    void ChangePinCode(int newPinCode);

    int GetBalance();

    IReadOnlyCollection<string> GetOperations();

    void Logout();
}
