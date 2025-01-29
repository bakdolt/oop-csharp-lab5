using Lab5.Application.Contracts.AdminContracts;

namespace Lab5.Presentation.Console.Runners;

public class AdminModeRunner : IRunner
{
    private readonly IAdminService adminService;

    public AdminModeRunner(IAdminService adminService)
    {
        this.adminService = adminService;
    }

    public void Run()
    {
        while (true)
        {
            System.Console.WriteLine("You choiced admin mode.\n Select option: 1 - change system password, 2 - logout");

            int cmd;
            int.TryParse(System.Console.ReadLine(), out cmd);

            switch (cmd)
            {
                case 1:
                    System.Console.WriteLine("Enter new system password:");
                    string? newPassword = System.Console.ReadLine();
                    if (newPassword != null)
                    {
                        adminService.ChangePassword(newPassword);
                        System.Console.WriteLine("password cannot be null, try again");
                    }
                    else
                    {
                        System.Console.WriteLine("password cannot be null, try again");
                    }

                    break;
                case 2:
                    adminService.Logout();
                    return;
                default:
                    System.Console.WriteLine("Invalid command, try again");
                    continue;
            }
        }
    }
}
