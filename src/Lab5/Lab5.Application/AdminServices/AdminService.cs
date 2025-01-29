using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts;
using Lab5.Application.Contracts.AdminContracts;
using Lab5.Application.Models.AdminModels;

namespace Lab5.Application.AdminServices;

public class AdminService : IAdminService
{
    private readonly CurrentAdminService currentAdminService;
    private readonly IAdminRepository adminRepository;
    private readonly PasswordHasher passwordHasher;

    public AdminService(CurrentAdminService currentAdminService, IAdminRepository adminRepository)
    {
        this.currentAdminService = currentAdminService;
        this.adminRepository = adminRepository;
        passwordHasher = new PasswordHasher();
    }

    public LoginResult Login(string password)
    {
        long actualHash = passwordHasher.GetHash(password);
        long expectedHash = adminRepository.GetSystemPasswordHashCode();

        if (actualHash != expectedHash)
        {
            return new LoginResult.Failure();
        }

        currentAdminService.Admin = new Admin(actualHash);
        return new LoginResult.Success();
    }

    public void ChangePassword(string newPassword)
    {
        if (currentAdminService.Admin == null) return;

        currentAdminService.Admin.ChangePassword(passwordHasher.GetHash(newPassword));

        adminRepository.UpdateSystemPassword(currentAdminService.Admin);
    }

    public void Logout()
    {
        currentAdminService.Admin = null;
    }
}
