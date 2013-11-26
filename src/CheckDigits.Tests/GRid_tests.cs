using NUnit.Framework;

namespace CheckDigits.Tests
{
	[TestFixture]
	public class GRid_tests
	{
		[Test]
		[TestCase("A12425GABC1234002M")]
		[TestCase("A12589KCCC2586482V")]
		[TestCase("A11343443434344344")]
		[TestCase("A1-2425G-ABC1234002-M")]
		[TestCase("GRid:A1-2425G-ABC1234002-M")]
		public void valid_grids (string grid)
		{
			var result = CheckDigits.GRid.IsValid(grid);

			Assert.That(result, Is.True, "Did not pass a valid GRid");
		}

		
		[Test]
		[TestCase("A12425GABC1234002X")] // bad check digit
		[TestCase("XX2425GABC1234002M")] // bad identifier
		[TestCase("A12425GABC12302M")]   // truncated
		public void invalid_grids (string grid)
		{
			var result = CheckDigits.GRid.IsValid(grid);

			Assert.That(result, Is.False, "Passed an invalid GRid");
		}
	}
}
