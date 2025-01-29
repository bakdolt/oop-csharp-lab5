namespace Lab5.Application;

internal class PasswordHasher
{
    private const long A = 57;
    private const long B = 19;
    private const long Mod = 9_999_999_996_423;

    /// <summary>
    /// Создает хэш из строки или числа
    /// </summary>
    /// <returns>Хэшированный пароль в виде 64-битного числа.</returns>
    public long GetHash(object password)
    {
        string? input = password.ToString();

        if (input == null)
        {
            throw new Exception("password is null");
        }

        long currentHash = 0;

        foreach (char key in input)
        {
            currentHash = ((currentHash * A) + (key * B)) % Mod;
        }

        return currentHash;
    }
}
