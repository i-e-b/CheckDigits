using System.Collections.Generic;

namespace CheckDigits.Internal;

internal static class DammQuasigroups
{
    /// <summary>
    /// The most common Damm quasigroup, able to find all 'phonetic' errors
    /// </summary>
    public static readonly int[,] Primary =
    {
        { 0, 3, 1, 7, 5, 9, 8, 6, 4, 2 },
        { 7, 0, 9, 2, 1, 5, 4, 8, 6, 3 },
        { 4, 2, 0, 6, 8, 7, 1, 3, 5, 9 },
        { 1, 7, 5, 0, 9, 8, 3, 4, 2, 6 },
        { 6, 1, 2, 3, 0, 4, 5, 9, 7, 8 },
        { 3, 6, 7, 4, 2, 0, 9, 5, 8, 1 },
        { 5, 8, 6, 9, 7, 2, 0, 1, 3, 4 },
        { 8, 9, 4, 5, 3, 6, 2, 0, 1, 7 },
        { 9, 4, 3, 8, 6, 1, 7, 2, 0, 5 },
        { 2, 5, 8, 1, 4, 3, 6, 7, 9, 0 }
    };

    /// <summary>
    /// A secondary quasigroup, using the digonal method
    /// </summary>
    public static readonly int[,] Secondary =
    {
        { 0, 2, 3, 4, 5, 6, 7, 8, 9, 1 },
        { 2, 0, 4, 1, 7, 9, 5, 3, 8, 6 },
        { 3, 7, 0, 5, 2, 8, 1, 6, 4, 9 },
        { 4, 1, 8, 0, 6, 3, 9, 2, 7, 5 },
        { 5, 6, 2, 9, 0, 7, 4, 1, 3, 8 },
        { 6, 9, 7, 3, 1, 0, 8, 5, 2, 4 },
        { 7, 5, 1, 8, 4, 2, 0, 9, 6, 3 },
        { 8, 4, 6, 2, 9, 5, 3, 0, 1, 7 },
        { 9, 8, 5, 7, 3, 1, 6, 4, 0, 2 },
        { 1, 3, 9, 6, 8, 4, 2, 7, 5, 0 }
    };

    /// <summary>
    /// Get the check digit for a value using the given quasigroup
    /// </summary>
    public static int CheckDigit(List<int> digits, int endOffset, int[,] quasigroup)
    {
        var sum = 0;
        var end = digits.Count - endOffset;

        for (var index = 0; index < end; index++)
        {
            var digit = digits[index];
            sum = quasigroup[sum, digit];
        }

        return sum;
    }
}