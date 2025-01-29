using Lab5.Application.Contracts;
using Lab5.Application.Contracts.AdminContracts;
using Lab5.Application.Contracts.UserContracts;

namespace Lab5.Presentation.Console.Runners;

public class MainRunner : IRunner
{
    private readonly IAdminService adminService;
    private readonly AdminModeRunner adminModeRunner;
    private readonly UserModeAuthorizationRunner userModeAuthorizationRunner;

    public MainRunner(IUserService userService, IAdminService adminService)
    {
        this.adminService = adminService;
        adminModeRunner = new AdminModeRunner(adminService);
        userModeAuthorizationRunner = new UserModeAuthorizationRunner(userService);
    }

    public void Run()
    {
        while (true)
        {
            System.Console.WriteLine("ATM system");
            System.Console.WriteLine("Select mode: 1 - User mode, 2 - Admin mode, 3 - finish work");
            System.Console.WriteLine("Enter your choice:");

            int cmd;
            int.TryParse(System.Console.ReadLine(), out cmd);

            switch (cmd)
            {
                case 1:
                    userModeAuthorizationRunner.Run();
                    break;
                case 2:
                    System.Console.WriteLine("Enter system password");
                    string? systemPassword = System.Console.ReadLine();

                    if (systemPassword == null) return;

                    LoginResult loginResult = adminService.Login(systemPassword);

                    if (loginResult is LoginResult.Success)
                    {
                        adminModeRunner.Run();
                    }
                    else
                    {
                        return;
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
