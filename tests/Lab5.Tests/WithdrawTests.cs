using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.UserContracts;
using Lab5.Application.Models.UserModels;
using Lab5.Application.UserServices;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class WithdrawTests
{
    [Fact]
    public void TestSuccesfulWithdraw()
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

        int withdrawAmount = 60;
        int expectedBalance = balance - withdrawAmount;

        userRepositoryMock.Setup(repo => repo.UpdateUserMoney(It.IsAny<User>())).Verifiable();

        // Act
        userService.Withdraw(withdrawAmount);

        // Assert
        userRepositoryMock.Verify(repo => repo.UpdateUserMoney(It.Is<User>(u => u.Balance == expectedBalance)), Times.Once());
    }

    [Fact]
    public void TestFailureWithdraw()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var currentUserService = new CurrentUserService();
        var userService = new UserService(currentUserService, userRepositoryMock.Object);

        int accountNumber = 520582;
        long hashedPassword = 95105905105;
        int balance = 100;
        var user = new User(accountNumber, hashedPassword, balance);
        currentUserService.User = user;

        int withdrawAmount = 150;

        // Act
        WithdrawResult result = userService.Withdraw(withdrawAmount);

        // Assert
        Assert.True(result is WithdrawResult.Failure);
    }
}
