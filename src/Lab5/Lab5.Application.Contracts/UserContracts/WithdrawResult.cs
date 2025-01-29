namespace Lab5.Application.Contracts.UserContracts;

public abstract record WithdrawResult
{
    private WithdrawResult() { }

    public sealed record Success : WithdrawResult;

    public sealed record Failure : WithdrawResult;
}
