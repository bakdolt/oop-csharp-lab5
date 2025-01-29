using Lab5.Application.Contracts.UserContracts;

namespace Lab5.Presentation.Console.Runners;

public class UserModeRunner : IRunner
{
    private readonly IUserService userService;

    public UserModeRunner(IUserService userService)
    {
        this.userService = userService;
    }

    public void Run()
    {
        int amount;
        while (true)
        {
            System.Console.WriteLine("You are logged in as a user.");
            System.Console.WriteLine("Select option: 1 - deposit, 2 - withdraw, 3 - change pin, 4 - get balance, 5 - show operations, 6 - logout");

            int cmd;
            int.TryParse(System.Console.ReadLine(), out cmd);

            switch (cmd)
            {
                case 1:
                    System.Console.WriteLine("Enter the amount:");
                    int.TryParse(System.Console.ReadLine(), out amount);

                    userService.Deposit(amount);

                    break;
                case 2:
                    System.Console.WriteLine("Enter the amount:");
                    int.TryParse(System.Console.ReadLine(), out amount);

                    WithdrawResult result = userService.Withdraw(amount);
                    if (result is WithdrawResult.Failure)
                    {
                        System.Console.WriteLine("Cannot withdraw money");
                    }

                    break;
                case 3:
                    System.Console.WriteLine("Enter new pin code:");

                    int pin = -1;
                    int.TryParse(System.Console.ReadLine(), out pin);

                    userService.ChangePinCode(pin);

                    break;
                case 4:
                    System.Console.WriteLine($"Current balance: {userService.GetBalance()}");
                    break;
                case 5:
                    IReadOnlyCollection<string> operations = userService.GetOperations();

                    foreach (string operation in operations)
                    {
                        System.Console.WriteLine($"{operation}");
                    }

                    break;
                case 6:
                    return;
                default:
                    System.Console.WriteLine("Invalid command, try again");
                    continue;
            }
        }
    }
}
