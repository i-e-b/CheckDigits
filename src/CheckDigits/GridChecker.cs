using System.Linq;

namespace CheckDigits
{
	public class GridChecker : IValidate, IProduceChecksum
	{
		public bool IsValid(string grid)
		{
			var input = grid.ToUpperInvariant();
			var skip = (input.StartsWith("GRID:")) ? (5) : (0);

			var a = input.Skip(skip).Where(Iso7064.AlphanumericCharSet.Contains).ToArray();
			var b = string.Concat(a.Take(17));
			var c = string.Concat(a.Last());

			if (a.Length != 18) return false;

			var x = Checksum(b);
			return c ==x;
		}

		/// <summary>
		/// This is the algorithm EXACTLY as prescribed by the IFPI
		/// </summary>
		public string Checksum(string input)
		{
			int p = 36;
			var ip = input.ToArray();

			int sj = 0;
			foreach (var a in ip)
			{
				var na = Iso7064.AlphanumericCharSet.IndexOf(a);
				var s = (p % 37) + (na);
				sj = (s == 36) ? (36) : (s % 36);

				p = 2 * sj;
			}
			p = (2 * sj) % 37;

			return string.Concat(Iso7064.AlphanumericCharSet[37 - p]);
		}
	}
}