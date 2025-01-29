namespace Lab5.Application.Contracts.UserContracts;

public abstract record RegisterResult
{
    private RegisterResult() { }

    public sealed record Success : RegisterResult;

    public sealed record Failure : RegisterResult;
}