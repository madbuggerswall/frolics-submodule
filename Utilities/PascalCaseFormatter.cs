using System.Globalization;

namespace Frolics.Utilities {
	public static class PascalCaseFormatter {
		public static string ToPascalCase(string text) {
			string[] words = text.Split('.', ',', '_', '-', ' ');
			return words.Length switch {
				1 => words[0],
				0 => "EmptyName",
				_ => CreatePascalCaseName(words)
			};
		}

		private static string CreatePascalCaseName(string[] words) {
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			for (int i = 0; i < words.Length; i++)
				if (!string.IsNullOrWhiteSpace(words[i]))
					words[i] = textInfo.ToTitleCase(words[i].Trim());

			return string.Join("", words);
		}
	}
}
