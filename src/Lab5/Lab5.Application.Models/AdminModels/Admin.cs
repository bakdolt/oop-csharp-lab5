namespace Lab5.Application.Models.AdminModels;

public class Admin
{
    public long HashedPassword { get; private set; }

    public Admin(long hashedPassword)
    {
        HashedPassword = hashedPassword;
    }

    public void ChangePassword(long newHashedPassword)
    {
        HashedPassword = newHashedPassword;
    }
}
