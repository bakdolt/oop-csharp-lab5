using Lab5.Application.AdminServices;
using Lab5.Application.UserServices;
using Lab5.Infrastructure.DataAccess;
using Lab5.Infrastructure.DataAccess.Repositories;
using Lab5.Presentation.Console.Runners;

namespace Itmo.ObjectOrientedProgramming.Lab5;

public class Program
{
    public static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres";
        var connectionManager = new CurrentConnectionManager();
        connectionManager.SetConnectionString(connectionString);

        string systempassword = "admin";
        var initializer = new DataBaseInitializer(connectionManager, systempassword);
        initializer.InitializeDatabase();

        var userRepository = new UserRepository(connectionManager);
        var adminRepository = new AdminRepository(connectionManager);

        var userService = new UserService(new CurrentUserService(), userRepository);
        var adminService = new AdminService(new CurrentAdminService(), adminRepository);

        var runner = new MainRunner(userService, adminService);

        runner.Run();

        connectionManager.CloseConnection();
    }
}