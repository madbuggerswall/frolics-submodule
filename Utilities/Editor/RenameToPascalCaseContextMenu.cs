using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace Frolics.Utilities.Editor {
	public static class RenameToPascalCaseContextMenu {
		[MenuItem("GameObject/Tools/Rename To PascalCase", false)]
		private static void RenameToPascalCase(MenuCommand menuCommand) {
			GameObject gameObject = menuCommand.context as GameObject;
			if (gameObject == null)
				return;

			Undo.RecordObject(gameObject, "Rename To PascalCase");

			gameObject.name = RenameToPascalCase(gameObject.name);
		}

		[MenuItem("GameObject/Tools/Rename To PascalCase Recursive", false)]
		private static void RenameToPascalCaseRecursive(MenuCommand menuCommand) {
			GameObject gameObject = menuCommand.context as GameObject;
			if (gameObject == null)
				return;

			Undo.RegisterFullObjectHierarchyUndo(gameObject, "Rename To PascalCase Recursive");
			RenameRecursive(gameObject.transform);
		}


		private static void RenameRecursive(Transform transform) {
			transform.gameObject.name = RenameToPascalCase(transform.gameObject.name);
			for (int i = 0; i < transform.childCount; i++)
				RenameRecursive(transform.GetChild(i));
		}

		private static string RenameToPascalCase(string text) {
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
