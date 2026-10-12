using NUnit.Framework;

namespace CheckDigits.Tests;

[TestFixture]
public class LuhnCheckTests
{
    [Test]
    // IMEIs
    [TestCase("869595062610968")]
    [TestCase("862205059131948")]
    [TestCase("862205059140741")]
    [TestCase("862205059263014")]
    [TestCase("862205059262289")]
    // ICCIDs
    [TestCase("89464278206107586729")]
    [TestCase("8949226230104167904")]
    [TestCase("89464278206107586992")]
    [TestCase("8935711550000913924")]
    [TestCase("89882390000490296038")]
    public void matches_correct_ids(string correctId)
    {
        Assert.That(CheckDigits.Luhn.IsValid(correctId), Is.True);
    }

    [Test]
    [TestCase("8949226230104167904")]
    [TestCase("8949226230104167094")] // <-- note this has '90' swapped to '09'
    public void weakness_demo(string correctId)
    {
        Assert.That(CheckDigits.Luhn.IsValid(correctId), Is.True);
    }

    [Test]
    // Same as 'correct' tests, but with broken digits
    // IMEIs
    [TestCase("86959506261096")]
    [TestCase("862205059131949")]
    [TestCase("862205059140742")]
    [TestCase("862205059263015")]
    [TestCase("862205059262288")]
    // ICCIDs
    [TestCase("89464278206107586728")]
    [TestCase("8949226302204167904")]
    [TestCase("89464278506107586992")]
    [TestCase("8935711550020913924")]
    [TestCase("8988239000090296038")]
    public void rejects_incorrect_ids(string wrongId)
    {
        Assert.That(CheckDigits.Luhn.IsValid(wrongId), Is.False);
    }

    [Test]
    [TestCase("imei: 869595067003094")]
    [TestCase(" 869595063551252. ")]
    [TestCase("862205059314924\r\n")]
    [TestCase("\r\n869595062611719")]
    [TestCase("89 46 427 820610758683 6")]
    public void non_decimal_digits_are_ignored(string correctId)
    {
        Assert.That(CheckDigits.Luhn.IsValid(correctId), Is.True);
    }
}