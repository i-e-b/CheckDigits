using CheckDigits.Internal;

namespace CheckDigits
{
	/// <summary>
	/// Helper for accessing check digit implementations
	/// </summary>
	public static class CheckDigits
	{
		/// <summary>
		/// IFPI GRid codes
		/// </summary>
		public static GridChecker GRid => new();

		/// <summary>
		/// Luhn algorithm, used in a wide range of applications including credit card numbers, IMEI/ICCIDs, and multiple government IDs.
		/// </summary>
		public static LuhnCheck Luhn => new();

		/// <summary>
		/// The Damm algorithm detects all single-digit errors and all adjacent transposition errors. It was presented by H. Michael Damm in 2004
		/// </summary>
		public static DammCheck Damm => new();

		/// <summary>
		/// A two-digit variant of the Damm algorithm that uses a pair of look-up tables
		/// </summary>
		public static DoubleDammCheck DoubleDamm => new();
	}
}
