#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Frolics.UI.Editor {
	[CustomEditor(typeof(MaskView))]
	public class MaskViewEditor : UnityEditor.Editor {
		private UnityEditor.Editor spriteRendererEditor;
		private UnityEditor.Editor spriteMaskEditor;

		public override void OnInspectorGUI() {
			// Draw MaskView’s own fields
			DrawDefaultInspector();

			DrawSpriteRendererEditor();
			DrawSpriteMaskEditor();
			ToggleHiddenChildren();
		}

		private void DrawSpriteRendererEditor() {
			MaskView maskView = (MaskView) target;
			SpriteRenderer spriteRenderer = maskView.GetSpriteRenderer();
			if (spriteRenderer == null)
				return;

			CreateCachedEditor(spriteRenderer, null, ref spriteRendererEditor);
			EditorGUILayout.Space();
			EditorGUILayout.LabelField(spriteRenderer.name, EditorStyles.boldLabel);
			spriteRendererEditor.OnInspectorGUI();
		}

		private void DrawSpriteMaskEditor() {
			MaskView maskView = (MaskView) target;
			SpriteMask spriteMask = maskView.GetSpriteMask();
			if (spriteMask == null)
				return;

			CreateCachedEditor(spriteMask, null, ref spriteMaskEditor);
			EditorGUILayout.Space();
			EditorGUILayout.LabelField(spriteMask.name, EditorStyles.boldLabel);
			spriteRendererEditor.OnInspectorGUI();
		}

		private void ToggleHiddenChildren() {
			if (!GUILayout.Button("Toggle Hidden Children"))
				return;

			for (int i = 0; i < targets.Length; i++) {
				MaskView imageView = (MaskView) targets[i];
				Undo.RecordObject(imageView, "Toggle Hidden Children");
				SpriteRenderer spriteRenderer = imageView.GetSpriteRenderer();
				spriteRenderer.gameObject.hideFlags ^= HideFlags.HideInHierarchy;
				spriteRenderer.transform.hideFlags ^= HideFlags.NotEditable;
			}
		}
	}
}

#endif
