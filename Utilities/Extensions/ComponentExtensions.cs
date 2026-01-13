using UnityEditor;
using UnityEngine;

namespace Frolics.Utilities.Extensions {
	public static class ComponentExtensions {
		public static T CopyTo<T>(this T source, GameObject target) where T : Component {
			// Add a new component of the same type to the target
			T copy = target.AddComponent<T>();

			// Copy serialized fields
			SerializedObject sourceSO = new(source);
			SerializedObject copySO = new(copy);

			SerializedProperty prop = sourceSO.GetIterator();
			bool enterChildren = true;
			while (prop.NextVisible(enterChildren)) {
				enterChildren = false;
				if (prop.name == "m_Script")
					continue; // skip script reference

				copySO.CopyFromSerializedProperty(prop);
			}

			copySO.ApplyModifiedProperties();
			Debug.Log($"{source.GetType()} copied from {source.name} to {target.name}");

			return copy;
		}
	}
}
