using Lab5.Application.Models.AdminModels;

namespace Lab5.Application.Contracts.AdminContracts;

public interface ICurrentAdminService
{
    Admin? Admin { get; set; }
}
