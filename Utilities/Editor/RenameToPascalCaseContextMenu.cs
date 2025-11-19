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
			gameObject.name = PascalCaseFormatter.ToPascalCase(gameObject.name);
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
			transform.gameObject.name = PascalCaseFormatter.ToPascalCase(transform.gameObject.name);
			for (int i = 0; i < transform.childCount; i++)
				RenameRecursive(transform.GetChild(i));
		}
	}
}
