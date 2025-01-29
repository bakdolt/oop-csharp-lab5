using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.UserModels;
using Lab5.Application.UserServices;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class DepositTest
{
    [Fact]
    public void TestSuccessfulDeposit()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var currentUserService = new CurrentUserService();
        var userService = new UserService(currentUserService, userRepositoryMock.Object);

        int accountNumber = 100013;
        long hashedPassword = 93091409;
        int balance = 100;
        var user = new User(accountNumber, hashedPassword, balance);
        currentUserService.User = user;

        int depositAmount = 60;
        int expectedBalance = balance + depositAmount;

        userRepositoryMock.Setup(repo => repo.UpdateUserMoney(It.IsAny<User>())).Verifiable();

        // Act
        userService.Deposit(depositAmount);

        // Assert
        userRepositoryMock.Verify(repo => repo.UpdateUserMoney(It.Is<User>(u => u.Balance == expectedBalance)), Times.Once());
    }
}
