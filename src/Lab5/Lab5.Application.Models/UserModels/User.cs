namespace Lab5.Application.Models.UserModels;

public class User
{
    public int AccountNumber { get; }

    public long HashedPinCode { get; private set; }

    public int Balance { get; private set; }

    public User(int accountNumber, long hashedPinCode, int balance)
    {
        AccountNumber = accountNumber;
        HashedPinCode = hashedPinCode;
        Balance = balance;
    }

    public void Deposit(int amount)
    {
        Balance += amount;
    }

    public void Withdraw(int amount)
    {
        Balance -= amount;
    }

    public void ChangePinCode(long newHashedPinCode)
    {
        HashedPinCode = newHashedPinCode;
    }
}
