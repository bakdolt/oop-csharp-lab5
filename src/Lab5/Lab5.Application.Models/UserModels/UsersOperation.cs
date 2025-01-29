namespace Lab5.Application.Models.UserModels;

public abstract record UsersOperation
{
    private UsersOperation() { }

    public sealed record Withdraw(int Amount) : UsersOperation
    {
        public override string ToString() => $"Withdraw {Amount}";
    }

    public sealed record Deposit(int Amount) : UsersOperation
    {
        public override string ToString() => $"Deposit {Amount}";
    }
}
