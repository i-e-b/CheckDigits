namespace CheckDigits.Internal;

/// <summary>
/// Damm algorithm
/// </summary>
public class DammCheck : IValidate, IProduceChecksum
{
    /// <inheritdoc />
    public bool IsValid(string input)
    {
        var digits = input.GetDecimalDigits();
        if (digits.Count < 1) return false;

        var expected = DammQuasigroups.CheckDigit(digits, 1, DammQuasigroups.Primary);

        return digits[digits.Count - 1] == expected;
    }

    /// <inheritdoc />
    public string Checksum(string input)
    {
        var digits = input.GetDecimalDigits();
        var sum    = DammQuasigroups.CheckDigit(digits, 0, DammQuasigroups.Primary);

        digits.Add(sum);
        return digits.ToDecimalString();
    }
}