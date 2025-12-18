using UnityEditor;
using UnityEngine;

namespace Frolics.UI.Editor {
	public static class CreateUIMenu {
		[MenuItem("GameObject/Frolics/UI/CanvasView", false)]
		private static void CreateCanvasView(MenuCommand menuCommand) {
			// Create parent GameObject with RectTransform
			GameObject gameObject = new("CanvasView", typeof(RectTransform));
			GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);
			gameObject.layer = LayerMask.NameToLayer("UI");
			gameObject.AddComponent<CanvasView>();

			// Register undo operations
			Undo.RegisterCreatedObjectUndo(gameObject, "Create CanvasView");

			// Select the new parent
			Selection.activeObject = gameObject;
		}
		
		[MenuItem("GameObject/Frolics/UI/ImageView", false)]
		private static void CreateImageView(MenuCommand menuCommand) {
			// Create parent GameObject with RectTransform
			GameObject gameObject = new("ImageView", typeof(RectTransform), typeof(ImageView));
			gameObject.layer = LayerMask.NameToLayer("UI");
			GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);

			// Register undo operations
			Undo.RegisterCreatedObjectUndo(gameObject, "Create ImageView");

			// Select the new parent
			Selection.activeObject = gameObject;
		}

		[MenuItem("GameObject/Frolics/UI/MaskView", false)]
		private static void CreateMaskView(MenuCommand menuCommand) {
			// Create parent GameObject with RectTransform
			GameObject gameObject = new("MaskView", typeof(RectTransform), typeof(MaskView));
			gameObject.layer = LayerMask.NameToLayer("UI");
			GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);

			// Register undo operations
			Undo.RegisterCreatedObjectUndo(gameObject, "Create MaskView");

			// Select the new parent
			Selection.activeObject = gameObject;
		}

		[MenuItem("GameObject/Frolics/UI/TextView", false)]
		private static void CreateTextView(MenuCommand menuCommand) {
			// Create parent GameObject with TextView + RectTransform
			GameObject gameObject = new GameObject("TextView", typeof(RectTransform), typeof(TextView));
			gameObject.layer = LayerMask.NameToLayer("UI");
			GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);
			
			// Register undo operations
			Undo.RegisterCreatedObjectUndo(gameObject, "Create TextView");

			// Select the new parent
			Selection.activeObject = gameObject;
		}

		[MenuItem("GameObject/Frolics/UI/ButtonView", false)]
		private static void CreateButtonView(MenuCommand menuCommand) {
			// Create parent GameObject with TextView + RectTransform
			GameObject gameObject = new GameObject("ButtonView", typeof(RectTransform), typeof(ButtonView));
			gameObject.layer = LayerMask.NameToLayer("UI");
			GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);
			
			// Register undo operations
			Undo.RegisterCreatedObjectUndo(gameObject, "Create ButtonView");

			// Select the new parent
			Selection.activeObject = gameObject;
		}
	}
}
