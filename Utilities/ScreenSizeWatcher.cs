using System;
using UnityEditor;
using UnityEngine;

namespace Frolics.Utilities {
	[InitializeOnLoad]
	public static class ScreenSizeWatcher {
		private static Vector2Int currentSize;
		public static event Action<Vector2Int> OnScreenSizeChange;

		static ScreenSizeWatcher() {
			EditorApplication.update += CheckSize;
			Application.onBeforeRender += CheckSize;
			
		}

		private static void CheckSize() {
			if (currentSize.x == Screen.width && currentSize.y == Screen.height)
				return;

			currentSize = new Vector2Int(Screen.width, Screen.height);
			OnScreenSizeChange?.Invoke(currentSize);
		}
	}
}
