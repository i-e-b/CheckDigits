using System.Collections.Generic;
using System.Linq;

namespace CheckDigits
{
	public class GridChecker : IValidate, IProduceChecksum
	{
		private const string Lookup = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <inheritdoc />
		public bool IsValid(string grid)
		{
			var input = grid.ToUpperInvariant();
			var skip = (input.StartsWith("GRID:")) ? (5) : (0);

			var a = input.Skip(skip).Where(Lookup.Contains).ToArray();
			var b = a.Take(17);
			var c = a.Last();

			if (a.Length != 18) return false;

			var x = CheckDigit(b);
			return c == x;
		}

		private static char CheckDigit(IEnumerable<char> input)
		{
			// This is the algorithm EXACTLY as prescribed by the IFPI
			var p  = 36;
			var ip = input.ToArray();

			foreach (var a in ip)
			{
				var s  = p + (Lookup.IndexOf(a));
				var sj = (s == 36) ? (36) : (s % 36);
				p = (2 * sj) % 37;
			}

			return Lookup[(37 - p) % 36];
		}

		/// <inheritdoc />
		public string Checksum(string input)
		{
			return input + CheckDigit(input);
		}
	}
}