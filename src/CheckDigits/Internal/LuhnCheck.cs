using System.Collections.Generic;

namespace CheckDigits.Internal;

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
        var digits   = code.GetDecimalDigits();
        var expected = CheckDigit(digits);

        return digits[digits.Count - 1] == expected;
    }

    /// <inheritdoc />
    public string Checksum(string input)
    {
        var digits   = input.GetDecimalDigits();
        var expected = CheckDigit(digits);

        return input + (char)('0' + expected);
    }

    private static int CheckDigit(List<int> digits)
    {
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
}