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
	}
}
