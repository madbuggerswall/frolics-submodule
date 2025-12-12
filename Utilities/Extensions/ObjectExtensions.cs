using System.Text;
using UnityEditor;
using UnityEngine;

namespace Frolics.Utilities.Extensions {
	public static class ObjectExtensions {
		public static void ListProperties(this Object component) {
			SerializedObject so = new(component);
			SerializedProperty prop = so.GetIterator();

			StringBuilder stringBuilder = new();
			while (prop.NextVisible(true))
				stringBuilder.AppendLine(prop.name);

			Debug.Log(stringBuilder.ToString());
		}
	}
}
