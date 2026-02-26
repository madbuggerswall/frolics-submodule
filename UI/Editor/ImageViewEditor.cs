#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Frolics.UI.Editor {
	[CustomEditor(typeof(ImageView))]
	public class ImageViewEditor : UnityEditor.Editor {
		private UnityEditor.Editor spriteRendererEditor;

		public override void OnInspectorGUI() {
			// Draw ImageView’s own fields
			base.OnInspectorGUI();

			DrawSpriteRendererEditor();
			ToggleHiddenChildrenButton();
		}

		private void DrawSpriteRendererEditor() {
			ImageView imageView = (ImageView) target;
			SpriteRenderer spriteRenderer = imageView.GetSpriteRenderer();
			if (spriteRenderer == null)
				return;

			CreateCachedEditor(spriteRenderer, null, ref spriteRendererEditor);
			EditorGUILayout.Space();
			EditorGUILayout.LabelField(spriteRenderer.name, EditorStyles.boldLabel);
			spriteRendererEditor.OnInspectorGUI();
		}

		private void ToggleHiddenChildrenButton() {
			if (!GUILayout.Button("Toggle Hidden Children"))
				return;

			ImageView imageView = (ImageView) target;
			Undo.RecordObject(imageView, "Toggle Hidden Children");
			SpriteRenderer spriteRenderer = imageView.GetSpriteRenderer();
			spriteRenderer.gameObject.hideFlags ^= HideFlags.HideInHierarchy;
			spriteRenderer.transform.hideFlags ^= HideFlags.NotEditable;
		}
	}
}

#endif
