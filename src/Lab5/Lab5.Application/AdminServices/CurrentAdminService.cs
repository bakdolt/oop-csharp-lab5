using Lab5.Application.Contracts.AdminContracts;
using Lab5.Application.Models.AdminModels;

namespace Lab5.Application.AdminServices;

public class CurrentAdminService : ICurrentAdminService
{
    public Admin? Admin { get; set; }
}
