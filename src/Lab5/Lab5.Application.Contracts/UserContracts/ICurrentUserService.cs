using Lab5.Application.Models.UserModels;

namespace Lab5.Application.Contracts.UserContracts;

public interface ICurrentUserService
{
    User? User { get; set; }
}
