using Frolics.Grids.SpatialHelpers;
using UnityEditor;
using UnityEngine;

namespace Frolics.Grids.Editor {
	[CustomPropertyDrawer(typeof(SquareCoord))]
	public class SquareCoordPropertyDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			SerializedProperty xProp = property.FindPropertyRelative("x");
			SerializedProperty yProp = property.FindPropertyRelative("y");

			Vector2Int vec = new(xProp.intValue, yProp.intValue);
			vec = EditorGUI.Vector2IntField(position, label, vec);

			xProp.intValue = vec.x;
			yProp.intValue = vec.y;

			EditorGUI.EndProperty();
		}
	}
}
