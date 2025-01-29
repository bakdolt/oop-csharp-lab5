using Lab5.Application.Contracts.UserContracts;
using Lab5.Application.Models.UserModels;

namespace Lab5.Application.UserServices;

public class CurrentUserService : ICurrentUserService
{
    public User? User { get; set; }
}
