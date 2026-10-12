namespace CheckDigits.Internal;

/// <summary>
/// A two-digit variant of the Damm algorithm that uses a pair of look-up tables
/// </summary>
public class DoubleDammCheck : IValidate, IProduceChecksum
{
    /// <inheritdoc />
    public bool IsValid(string input)
    {
        var digits = input.GetDecimalDigits();

        if (digits.Count < 2) return false;

        var first  = DammQuasigroups.CheckDigit(digits, 2, DammQuasigroups.Primary);
        var second = DammQuasigroups.CheckDigit(digits, 1, DammQuasigroups.Secondary);

        return (digits[digits.Count - 2] == first) && (digits[digits.Count - 1] == second);
    }

    /// <inheritdoc />
    public string Checksum(string input)
    {
        var digits   = input.GetDecimalDigits();

        var first  = DammQuasigroups.CheckDigit(digits, 0, DammQuasigroups.Primary);
        digits.Add(first);

        var second = DammQuasigroups.CheckDigit(digits, 0, DammQuasigroups.Secondary);
        digits.Add(second);

        return digits.ToDecimalString();
    }
}