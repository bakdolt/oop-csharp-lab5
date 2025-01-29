namespace Lab5.Application.Contracts.AdminContracts;

public interface IAdminService
{
    LoginResult Login(string password);

    void ChangePassword(string newPassword);

    void Logout();
}
