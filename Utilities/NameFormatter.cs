using System.Globalization;
using System.Text.RegularExpressions;

namespace Frolics.Utilities {
	public static class NameFormatter {
		/// <summary>
		/// Splits on capitals and delimiters, title‑cases each word, and joins with spaces.
		/// <example>
		/// "thisIs_an-example" → "This Is An Example"
		/// </example>
		/// </summary>
		public static string Nicify(string input) {
			string[] words = ToPascalCaseWords(input);
			return string.Join(" ", words);
		}

		/// <summary>
		/// Splits on capitals and delimiters, title‑cases each word, and joins them.
		/// <example>
		/// "thisIs_an-example" → "ThisIsAnExample"
		/// </example>
		/// </summary>
		public static string ToPascalCase(string input) {
			string[] words = ToPascalCaseWords(input);
			return string.Join("", words);
		}

		private static string[] ToPascalCaseWords(string input) {
			string[] words = SplitByCapitalsAndDelimiters(input);
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			for (int i = 0; i < words.Length; i++)
				if (!string.IsNullOrWhiteSpace(words[i]))
					words[i] = textInfo.ToTitleCase(words[i].Trim());

			return words;
		}

		private static string[] SplitByCapitals(string input) {
			// Split before each capital letter, but keep the letters
			return Regex.Split(input, @"(?=[A-Z])");
		}

		private static string[] SplitByCapitalsAndDelimiters(string input) {
			// Split before capitals OR on delimiters
			return Regex.Split(input, @"(?=[A-Z])|[.,_\-\s]+");
		}
	}
}
