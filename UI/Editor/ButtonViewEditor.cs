#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Frolics.UI.Editor {
	[CustomEditor(typeof(ButtonView))]
	[CanEditMultipleObjects]
	public class ButtonViewEditor : UnityEditor.Editor {
		private UnityEditor.Editor spriteRendererEditor;

		public override void OnInspectorGUI() {
			// Draw ButtonView’s own fields
			DrawDefaultInspector();

			SyncSizes();
		}

		private void SyncSizes() {
			for (int i = 0; i < targets.Length; i++) {
				ButtonView buttonView = (ButtonView) targets[i];
				BoxCollider2D collider = buttonView.GetCollider();
				if (collider == null)
					continue;

				// Example: keep size synced
				RectTransform rectTransform = buttonView.GetRectTransform();
				collider.size = rectTransform.rect.size;
			}
		}
	}
}

#endif
