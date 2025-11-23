using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Frolics.Utilities.Editor {
	public abstract class GenericManagedReferenceDrawer : PropertyDrawer {
		// Cache per subclass
		private List<Type> cachedTypes;

		// Each subclass defines its "valid" type
		protected abstract Type GetValidType();

		// Base class does not assume how filtering should work. Instead, it delegates the entire decision to the subclass
		protected abstract bool IsValidType(Type type);

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			// Ensure we only handle managed reference fields
			if (property.propertyType != SerializedPropertyType.ManagedReference) {
				EditorGUI.LabelField(position, label.text, "Not a managed reference");
				return;
			}

			// Draw the label (e.g. Element 0), Unity handles indentation and prefix
			position = EditorGUI.PrefixLabel(position, label);

			// Draw the dropdown to select/change type
			DrawTypeDropdown(position, property);

			// If a type is selected, draw its serialized fields inline
			if (property.managedReferenceValue != null) {
				DrawManagedReferenceChildren(position, property);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

			if (property.managedReferenceValue == null)
				return height;

			SerializedProperty iterator = property.Copy();
			SerializedProperty end = iterator.GetEndProperty();

			if (!iterator.NextVisible(true))
				return height;

			while (!SerializedProperty.EqualContents(iterator, end)) {
				height += EditorGUI.GetPropertyHeight(iterator, true) + EditorGUIUtility.standardVerticalSpacing;

				if (!iterator.NextVisible(false))
					break;
			}

			return height;
		}

		#region Type Selection Dropdown Menu

		private void DrawTypeDropdown(Rect position, SerializedProperty property) {
			Type currentType = GetCurrentType(property);
			Rect buttonRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

			// Button shows current type name or the default name
			Type validType = GetValidType();
			string defaultName = $"Select a {TypeNameFormatter.Nicify(validType)}";
			string name = currentType == null ? defaultName : NameFormatter.Nicify(currentType.Name);
			if (!GUI.Button(buttonRect, name, EditorStyles.popup))
				return;

			GenericMenu menu = new GenericMenu();
			AddNoneOption(menu, property, currentType);
			AddTypeOptions(menu, property, currentType);
			menu.ShowAsContext();
		}

		private static void AddNoneOption(GenericMenu menu, SerializedProperty property, Type currentType) {
			menu.AddItem(new GUIContent("None"), currentType == null, AddNoneMenuFunction(property));
		}

		private void AddTypeOptions(GenericMenu menu, SerializedProperty property, Type currentType) {
			List<Type> assignableTypes = GetAllValidTypes();

			for (int i = 0; i < assignableTypes.Count; i++) {
				Type type = assignableTypes[i];
				string typeName = NameFormatter.Nicify(type.Name);

				menu.AddItem(new GUIContent(typeName), type == currentType, AddTypeMenuFunction(property, type));
			}
		}

		private static GenericMenu.MenuFunction AddNoneMenuFunction(SerializedProperty property) {
			return () => {
				property.managedReferenceValue = null;
				property.serializedObject.ApplyModifiedProperties();
			};
		}

		private static GenericMenu.MenuFunction AddTypeMenuFunction(SerializedProperty property, Type type) {
			return () => {
				// Instantiate a new object of the chosen type
				property.managedReferenceValue = Activator.CreateInstance(type);
				property.serializedObject.ApplyModifiedProperties();
			};
		}

		#endregion

		private static void DrawManagedReferenceChildren(Rect position, SerializedProperty property) {
			SerializedProperty iterator = property.Copy();
			SerializedProperty end = iterator.GetEndProperty();
			float y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

			if (!iterator.NextVisible(true))
				return;

			EditorGUI.indentLevel++;

			while (!SerializedProperty.EqualContents(iterator, end)) {
				float height = EditorGUI.GetPropertyHeight(iterator, true);
				Rect childRect = new Rect(position.x, y, position.width, height);
				EditorGUI.PropertyField(childRect, iterator, true);

				y += height + EditorGUIUtility.standardVerticalSpacing;
				if (!iterator.NextVisible(false))
					break;
			}

			EditorGUI.indentLevel--;
		}

		private Type GetCurrentType(SerializedProperty property) {
			string fullTypeName = property.managedReferenceFullTypename;
			if (string.IsNullOrEmpty(fullTypeName))
				return null;

			string[] parts = fullTypeName.Split(' ');
			if (parts.Length != 2)
				return null;

			string assemblyName = parts[0];
			string className = parts[1];

			List<Type> validTypes = GetAllValidTypes();
			for (int i = 0; i < validTypes.Count; i++)
				if (validTypes[i].Assembly.GetName().Name == assemblyName && validTypes[i].FullName == className)
					return validTypes[i];

			return null;
		}

		private List<Type> GetAllValidTypes() {
			if (cachedTypes != null)
				return cachedTypes;

			cachedTypes = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			for (int i = 0; i < assemblies.Length; i++) {
				Type[] types = GetAllTypesInAssembly(assemblies[i]);
				CacheValidTypes(types);
			}

			return cachedTypes;
		}

		// If some types fail to load, ReflectionTypeLoadException is thrown.
		// e.Types contains the  loaded types (with nulls for the ones that failed)
		private static Type[] GetAllTypesInAssembly(Assembly assembly) {
			try {
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e) {
				return e.Types;
			}
		}

		private void CacheValidTypes(Type[] types) {
			for (int i = 0; i < types.Length; i++) {
				Type type = types[i];
				if (type == null)
					continue;

				if (IsValidType(type) && !type.IsAbstract && !type.IsInterface)
					cachedTypes.Add(type);
			}
		}
	}
}
#endif
