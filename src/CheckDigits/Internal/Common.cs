using System.Collections.Generic;
using System.Text;

namespace CheckDigits.Internal;

/// <summary>
/// Helpers for other classes
/// </summary>
public static class Common
{
    /// <summary>
    /// Return only the decimal digits from a string
    /// </summary>
    public static List<int> GetDecimalDigits(this string code)
    {
        var result = new List<int>();
        foreach (var c in code)
        {
            if (c is >= '0' and <= '9') result.Add(c - '0');
        }

        return result;
    }

    /// <summary>
    /// Return decimal string of digits in range 0..9
    /// </summary>
    public static string ToDecimalString(this List<int> digits)
    {
        var sb = new StringBuilder();

        foreach (var digit in digits)
        {
            sb.Append((char)('0' + digit));
        }

        return sb.ToString();
    }
}