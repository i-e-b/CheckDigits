namespace CheckDigits
{
	/// <summary>
	/// Interface for classes that validate an input containing a checksum
	/// </summary>
	public interface IValidate
	{
		/// <summary>
		/// Return <c>true</c> if the input string contains a valid checksum.
		/// Return <c>false</c> if the input is missing a checksum or the checksum is invalid.
		/// </summary>
		bool IsValid(string input);
	}
}