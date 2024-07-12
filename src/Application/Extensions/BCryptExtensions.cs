using BC = BCrypt.Net.BCrypt;

namespace Application.Extensions;

public static class BCryptExtensions
{
    public static bool PassWordCheck(this string requestPassword, string accountPassword) => BC.Verify(requestPassword, accountPassword);

    public static string HashPassWord(this string requestPassword) => BC.HashPassword(requestPassword);
}
