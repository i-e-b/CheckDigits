using System.Collections.Generic;

namespace CheckDigits;

/// <summary>
/// String checks for IDs using the Luhn algorithm.
/// This includes IMEIs and ICCIDs.
/// </summary>
public class LuhnCheck : IValidate, IProduceChecksum
{
    /// <summary>
    /// Returns <c>true</c> if the supplied code appears to have a valid Luhn checksum
    /// </summary>
    public bool IsValid(string code)
    {
        var digits   = GetDecimalDigits(code);
        var expected = CheckDigit(digits);

        return digits[digits.Count - 1] == expected;
        /*
        var sum      = 0;
        var parity   = digits.Count & 1;
        var end      = digits.Count - 1;

        for (int i = 0; i < end; i++)
        {
            var digit = digits[i];
            if ((i & 1) != parity) sum += digit;
            else if (digit > 4) sum += (2 * digit) - 9;
            else sum += (2 * digit);
        }

        var expected = (10 - (sum % 10)) % 10;
        return digits[end] == expected;*/
    }

    /// <inheritdoc />
    public string Checksum(string input)
    {
        throw new System.NotImplementedException();
    }

    private static int CheckDigit(List<int> digits)
    {
        //var digits = GetDecimalDigits(code);
        var sum    = 0;
        var parity = digits.Count & 1;
        var end    = digits.Count - 1;

        for (int i = 0; i < end; i++)
        {
            var digit = digits[i];
            if ((i & 1) != parity) sum += digit;
            else if (digit > 4) sum += (2 * digit) - 9;
            else sum += (2 * digit);
        }

        return (10 - (sum % 10)) % 10;
    }

    private static List<int> GetDecimalDigits(string code)
    {
        var result = new List<int>();
        foreach (var c in code)
        {
            if (c is >= '0' and <= '9') result.Add(c - '0');
        }

        return result;
    }
}