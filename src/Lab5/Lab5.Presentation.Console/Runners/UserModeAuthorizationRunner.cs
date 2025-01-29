using Lab5.Application.Contracts;
using Lab5.Application.Contracts.UserContracts;

namespace Lab5.Presentation.Console.Runners;

public class UserModeAuthorizationRunner : IRunner
{
    private readonly IUserService userService;
    private readonly UserModeRunner userModeRunner;

    public UserModeAuthorizationRunner(IUserService userService)
    {
        this.userService = userService;
        userModeRunner = new UserModeRunner(userService);
    }

    public void Run()
    {
        int accountNumber, pinCode;
        while (true)
        {
            System.Console.WriteLine("You choiced user mode.\n Select option: 1 - login, 2 - create new user, 3 - go back");
            System.Console.WriteLine("Enter your choice:");

            int cmd;
            int.TryParse(System.Console.ReadLine(), out cmd);

            switch (cmd)
            {
                case 1:
                    System.Console.WriteLine("Enter account number:");
                    int.TryParse(System.Console.ReadLine(), out accountNumber);

                    System.Console.WriteLine("Enter pin code:");
                    int.TryParse(System.Console.ReadLine(), out pinCode);

                    LoginResult loginResult = userService.Login(accountNumber, pinCode);

                    if (loginResult is LoginResult.Success)
                    {
                        userModeRunner.Run();
                    }
                    else
                    {
                        System.Console.WriteLine("Login failed");
                    }

                    break;
                case 2:
                    System.Console.WriteLine("Enter account number:");
                    int.TryParse(System.Console.ReadLine(), out accountNumber);

                    System.Console.WriteLine("Enter pin code:");
                    int.TryParse(System.Console.ReadLine(), out pinCode);

                    RegisterResult registerResult = userService.Register(accountNumber, pinCode);

                    if (registerResult is RegisterResult.Success)
                    {
                        userModeRunner.Run();
                    }
                    else
                    {
                        System.Console.WriteLine("Register failed");
                    }

                    break;
                case 3:
                    return;
                default:
                    System.Console.WriteLine("Invalid command, try again");
                    continue;
            }
        }
    }
}
