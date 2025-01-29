using Lab5.Application.Models.AdminModels;

namespace Lab5.Application.Abstractions.Repositories;

public interface IAdminRepository
{
    long GetSystemPasswordHashCode();

    void UpdateSystemPassword(Admin admin);
}
