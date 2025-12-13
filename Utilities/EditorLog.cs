using UnityEngine;

namespace Frolics.Utilities {
	public static class EditorLog {
		public static void Log(string message, bool editorOnly = true) {
			if (editorOnly && !Application.isEditor)
				return;

			Debug.Log(message);
		}

		public static void LogWarning(string message, bool editorOnly = true) {
			if (editorOnly && !Application.isEditor)
				return;

			Debug.LogWarning(message);
		}

		public static void LogError(string message, bool editorOnly = true) {
			if (editorOnly && !Application.isEditor)
				return;

			Debug.LogError(message);
		}
	}
}
