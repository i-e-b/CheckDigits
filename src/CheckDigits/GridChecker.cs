using System.Linq;

namespace CheckDigits
{
	public class GridChecker : IValidate, IProduceChecksum
	{
		const string lookup = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// Returns true for valid GRids, false otherwise
		/// </summary>
		public bool IsValid(string grid)
		{
			var input = grid.ToUpperInvariant();
			var skip = (input.StartsWith("GRID:")) ? (5) : (0);

			var a = input.Skip(skip).Where(lookup.Contains).ToArray();
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

			foreach (var a in ip)
			{
				var s = p + (lookup.IndexOf(a));
				var sj = (s == 36) ? (36) : (s % 36);
				p = (2 * sj) % 37;
			}
			
			return string.Concat(lookup[(37 - p) % 36]);
		}
	}
}