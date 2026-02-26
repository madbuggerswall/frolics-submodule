#if UNITY_EDITOR

using TMPro;
using UnityEditor;
using UnityEngine;

namespace Frolics.UI.Editor {
	[CustomEditor(typeof(TextView))]
	[CanEditMultipleObjects]
	public class TextViewEditor : UnityEditor.Editor {
		private UnityEditor.Editor textViewEditor;

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			DrawTextMeshProInspector();
			ToggleHiddenChildren();
		}

		private void DrawTextMeshProInspector() {
			for (int i = 0; i < targets.Length; i++) {
				TextView textView = (TextView) targets[i];
				TextMeshPro text = textView.GetText();
				if (text == null)
					continue;

				// Create an editor for the TextMeshPro
				if (textViewEditor == null || textViewEditor.target != text)
					textViewEditor = CreateEditor(text);

				EditorGUILayout.Space();
				EditorGUILayout.LabelField("Text Mesh Pro", EditorStyles.boldLabel);

				// Draw the default inspector for the TextMeshPro
				textViewEditor.OnInspectorGUI();
			}
		}

		private void ToggleHiddenChildren() {
			if (!GUILayout.Button("Toggle Hidden Children"))
				return;

			for (int i = 0; i < targets.Length; i++) {
				TextView textView = (TextView) targets[i];
				Undo.RecordObject(textView, "Toggle Hidden Children");
				TextMeshPro text = textView.GetText();
				text.gameObject.hideFlags ^= HideFlags.HideInHierarchy;
				text.transform.hideFlags ^= HideFlags.NotEditable;
			}
		}
	}
}

#endif
