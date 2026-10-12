namespace CheckDigits
{
	/// <summary>
	/// Interface for classes that produce a checksum from an input
	/// </summary>
	public interface IProduceChecksum
	{
		/// <summary>
		/// Return the checksum for an input code.
		/// This returns only the original code with the check digit added to the appropriate location.
		/// </summary>
		string Checksum(string input);
	}
}