using System;
using System.Linq;

namespace CheckDigits
{
	public class Iso7064
	{
		public const string NumericCharSet = "0123456789";
		public const string Mod112CharSet = "0123456789X";
		public const string HexCharSet = "0123456789ABCDEF";
		public const string AlphaCharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string AlphanumericCharSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string Mod372CharSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*";


		public static void GetCheckDigitRadixAndModulus(string charSet, bool doubleDigit, out int radix, out int modulus)
		{
			radix = charSet.Length;
			modulus = radix + 1;

			if (doubleDigit)
			{
				//The modulus numbers below for double digit calculations are defined by ISO 7064.
				switch (radix)
				{
					case 10:
						modulus = 97;
						break;
					case 16: //Mod 251,16 isn't defined in ISO 7064, but it could be useful so I added it anyway.
						modulus = 251;
						break;
					case 26:
						modulus = 661;
						break;
					case 36:
						modulus = 1271;
						break;
				}
			}
			else if (radix == 11)
			{
				//MOD 11,2 - Single digit 0-9 check with an added 'X' check digit.
				modulus = 11;
				radix = 2;
			}
			else if (radix == 37)
			{
				//MOD 37,2 - Single digit 0-9,A-Z check with an added '*' check digit.
				modulus = 37;
				radix = 2;
			}

			if (radix != 2 && radix != 10 && radix != 16 && radix != 26 && radix != 36)
				throw new ArgumentException("Invalid character set.", "charSet");
		} 

		//http://www.codeproject.com/Articles/16540/Error-Detection-Based-on-Check-Digit-Schemes
		public static string CalculateHybridSystemCheckDigit(string value, string charSet)
		{
			if (string.IsNullOrEmpty(value)) throw new ArgumentException("Null value provided", "value");
			if (string.IsNullOrEmpty(charSet)) throw new ArgumentException("Null charSet provided", "charSet");

			value = value.ToUpper();
			int radix = charSet.Length;
			int pos = radix;

			foreach (char c in value)
			{
				int i = charSet.IndexOf(c);

				if (i == -1)
					return null;

				pos += i;

				if (pos > radix)
					pos -= radix;

				pos *= 2;

				if (pos >= radix + 1)
					pos -= radix + 1;
			}

			pos = radix + 1 - pos;

			if (pos == radix)
				pos = 0;

			return string.Concat(charSet[pos]);
		}

		//https://github.com/danieltwagner/iso7064.
		public static string PureSystemDigit(string value, int radix, int modulus, string charSet, bool doubleDigit)
		{
			if (string.IsNullOrEmpty(value)) throw new ArgumentException("Null value provided", "value");
			if (string.IsNullOrEmpty(charSet)) throw new ArgumentException("Null charSet provided", "charSet");

			value = value.ToUpper();
			int p = 0;

			foreach (var i in value.Select(c => charSet.IndexOf(c)))
			{
				if (i == -1) throw new InvalidOperationException("Value contains invalid character");

				p = ((p + i) * radix) % modulus;
			}

			if (doubleDigit) p = (p * radix) % modulus;

			var checkDigit = (modulus - p + 1) % modulus;

			if (doubleDigit)
			{
				int second = checkDigit % radix;
				int first = (checkDigit - second) / radix;
				return string.Concat(charSet[first], charSet[second]);
			}
			return string.Concat(charSet[checkDigit]);
		}
	}
}