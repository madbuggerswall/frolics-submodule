using Frolics.Grids.SpatialHelpers;
using UnityEditor;
using UnityEngine;

namespace Frolics.Grids.Editor {
	[CustomPropertyDrawer(typeof(SquareCoord))]
	public class SquareCoordPropertyDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			// Get the x and y properties
			SerializedProperty xProp = property.FindPropertyRelative("x");
			SerializedProperty yProp = property.FindPropertyRelative("y");

			// Split the rect for two fields
			float fieldWidth = position.width / 2 - 2;

			// Draw X field
			EditorGUIUtility.labelWidth = 15;
			Rect xRect = new Rect(position.x, position.y, fieldWidth, position.height);
			EditorGUI.PropertyField(xRect, xProp, new GUIContent("X"));

			// Draw Y field
			Rect yRect = new Rect(position.x + fieldWidth + 4, position.y, fieldWidth, position.height);
			EditorGUI.PropertyField(yRect, yProp, new GUIContent("Y"));

			EditorGUIUtility.labelWidth = 0;
			EditorGUI.EndProperty();
		}
	}
}
