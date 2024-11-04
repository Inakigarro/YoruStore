namespace Tienda.Utilities.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string value) =>
         string.IsNullOrWhiteSpace(value);

    public static bool IsEqual(this string value, string compare) =>
        string.Equals(value, compare);
}