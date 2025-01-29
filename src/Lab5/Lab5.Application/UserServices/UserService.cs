using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts;
using Lab5.Application.Contracts.UserContracts;
using Lab5.Application.Models.UserModels;

namespace Lab5.Application.UserServices;

public class UserService : IUserService
{
    private readonly CurrentUserService currentUserService;
    private readonly IUserRepository userRepository;
    private readonly PasswordHasher passwordHasher;

    public UserService(CurrentUserService currentUserService, IUserRepository userRepository)
    {
        this.currentUserService = currentUserService;
        this.userRepository = userRepository;
        passwordHasher = new PasswordHasher();
    }

    public LoginResult Login(int accountNumber, int pinCode)
    {
        User? user = userRepository.FindUserByAccountNumber(accountNumber);

        if (user == null)
        {
            return new LoginResult.Failure();
        }

        if (user.HashedPinCode != passwordHasher.GetHash(pinCode))
        {
            return new LoginResult.Failure();
        }

        currentUserService.User = user;
        return new LoginResult.Success();
    }

    public RegisterResult Register(int accountNumber, int pinCode)
    {
        if (userRepository.FindUserByAccountNumber(accountNumber) != null)
        {
            return new RegisterResult.Failure();
        }

        var user = new User(accountNumber, passwordHasher.GetHash(pinCode), 0);

        userRepository.AddNewUser(user);

        currentUserService.User = user;

        return new RegisterResult.Success();
    }

    public void Deposit(int amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Amount must be positive");
        }

        if (currentUserService.User == null) return;

        currentUserService.User.Deposit(amount);

        userRepository.UpdateUserMoney(currentUserService.User);

        userRepository.AddOperation(currentUserService.User, new UsersOperation.Deposit(amount));
    }

    public WithdrawResult Withdraw(int amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Amount must be positive");
        }

        if (currentUserService.User == null) return new WithdrawResult.Failure();

        if (amount > currentUserService.User.Balance)
        {
            return new WithdrawResult.Failure();
        }

        currentUserService.User.Withdraw(amount);

        userRepository.UpdateUserMoney(currentUserService.User);

        userRepository.AddOperation(currentUserService.User, new UsersOperation.Withdraw(amount));

        return new WithdrawResult.Success();
    }

    public int GetBalance()
    {
        if (currentUserService.User == null)
        {
            throw new Exception("no current user");
        }

        return currentUserService.User.Balance;
    }

    public void ChangePinCode(int newPinCode)
    {
        if (currentUserService.User == null)
        {
            throw new Exception("no current user");
        }

        if (newPinCode < 0)
        {
            throw new Exception("pin code cannot contains '-'");
        }

        currentUserService.User.ChangePinCode(passwordHasher.GetHash(newPinCode));

        userRepository.UpdateUserPinCode(currentUserService.User);
    }

    public IReadOnlyCollection<string> GetOperations()
    {
        if (currentUserService.User == null)
        {
            throw new Exception("no current user");
        }

        return userRepository.GetOperations(currentUserService.User);
    }

    public void Logout()
    {
        currentUserService.User = null;
    }
}
