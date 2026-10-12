using System;
using NUnit.Framework;

namespace CheckDigits.Tests;

[TestFixture]
public class DammCheckTests
{
    [Test]
    [TestCase("95950626109", "959506261099")]
    [TestCase("code: 869595067003094", "8695950670030940")]
    [TestCase(" 869595063551252. ", "8695950635512522")]
    [TestCase("862205059314924\r\n", "8622050593149245")]
    [TestCase("\r\n869595062611719", "8695950626117192")]
    [TestCase("89 46 427 820610758683 6", "894642782061075868367")]
    public void sd_single_Damm_encode(string input, string expected)
    {
        var actual = CheckDigits.Damm.Checksum(input);
        Console.WriteLine(actual);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("959506261099")]
    [TestCase("8695950670030940")]
    [TestCase("code: 8695950635512522")]
    [TestCase("8622050593149245\r\n")]
    [TestCase("\r\n8695950626117192")]
    [TestCase("89-4642-78 20-6107-58 683-67")]
    public void sd_valid_codes_are_accepted(string code)
    {
        Assert.That(CheckDigits.Damm.IsValid(code), Is.True);
    }

    [Test]
    [TestCase("959506261909")]
    [TestCase("8695950670300940")]
    [TestCase("code: 8695905635512522")]
    [TestCase("8622005593149245\r\n")]
    [TestCase("\r\n6895950626117192")]
    [TestCase("89-4624-78 20-6107-58 683-67")]
    public void sd_transpositions_are_rejected(string code)
    {
        Assert.That(CheckDigits.Damm.IsValid(code), Is.False);
    }

    [Test]
    [TestCase("959506261098")]
    [TestCase("8695950670040940")]
    [TestCase("code: 8695990635512522")]
    [TestCase("8602050593149245\r\n")]
    [TestCase("\r\n8995950626117192")]
    [TestCase("89-4642-78 20-6117-58 683-67")]
    public void sd_single_errors_are_rejected(string code)
    {
        Assert.That(CheckDigits.Damm.IsValid(code), Is.False);
    }

    [Test]
    [TestCase("9595062610")]
    [TestCase("695950670030940")]
    [TestCase("code: 895950635512522")]
    //[TestCase("8622050 93149245\r\n")] // This one is not caught with single Damm, but is caught in the double case.
    [TestCase("860205059314925\r\n")]
    [TestCase("\r\n869595062617192")]
    [TestCase("89-4642-78 20-617-58 683-67")]
    public void sd_single_deletes_are_rejected(string code)
    {
        Assert.That(CheckDigits.Damm.IsValid(code), Is.False);
    }

    [Test]
    [TestCase("95950626109", "9595062610995")]
    [TestCase("code: 869595067003094", "86959506700309404")]
    [TestCase(" 869595063551252. ", "86959506355125229")]
    [TestCase("862205059314924\r\n", "86220505931492454")]
    [TestCase("\r\n869595062611719", "86959506261171923")]
    [TestCase("89 46 427 820610758683 6", "8946427820610758683674")]
    public void dd_double_Damm_encode(string input, string expected)
    {
        var actual = CheckDigits.DoubleDamm.Checksum(input);
        Console.WriteLine(actual);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("9595062610995")]
    [TestCase("86959506700309404")]
    [TestCase("code: 86959506355125229")]
    [TestCase("86220505931492454\r\n")]
    [TestCase("\r\n86959506261171923")]
    [TestCase("89-464-27 82-061-07 586-836-74")]
    public void dd_valid_codes_are_accepted(string code)
    {
        Assert.That(CheckDigits.DoubleDamm.IsValid(code), Is.True);
    }

    [Test]
    [TestCase("9595026610995")]
    [TestCase("86995506700309404")]
    [TestCase("code: 86959506355125292")]
    [TestCase("86220505931942454\r\n")]
    [TestCase("\r\n86959506621171923")]
    [TestCase("89-464-27 82-601-07 586-836-74")]
    public void dd_transpositions_are_rejected(string code)
    {
        Assert.That(CheckDigits.DoubleDamm.IsValid(code), Is.False);
    }

    [TestCase("9595063610995")]
    [TestCase("86959506710309404")]
    [TestCase("code: 86949506355125229")]
    [TestCase("86220505931482454\r\n")]
    [TestCase("\r\n86959506262171923")]
    [TestCase("89-464-27 82-061-07 586-736-74")]
    public void dd_single_errors_are_rejected(string code)
    {
        Assert.That(CheckDigits.DoubleDamm.IsValid(code), Is.False);
    }

    [Test]
    [TestCase("959506610995")]
    [TestCase("8699506700309404")]
    [TestCase("code: 8695950635525229")]
    [TestCase("8622050931492454\r\n")]
    [TestCase("\r\n8959506261171923")]
    [TestCase("89-464-27 82-061-07 56-836-74")]
    public void dd_single_deletes_are_rejected(string code)
    {
        Assert.That(CheckDigits.DoubleDamm.IsValid(code), Is.False);
    }
}