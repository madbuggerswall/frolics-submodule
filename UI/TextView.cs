using TMPro;
using UnityEngine;

namespace Frolics.UI {
	public class TextView : MonoBehaviour {
		[SerializeField, HideInInspector] private RectTransform rectTransform;
		[SerializeField, HideInInspector] private TextMeshPro text;

		private void Reset() => Initialize();

		private void Initialize() {
			if (rectTransform == null)
				rectTransform = GetComponent<RectTransform>();

			if (text == null)
				text = GetComponentInChildren<TextMeshPro>();

			if (rectTransform == null)
				rectTransform = gameObject.AddComponent<RectTransform>();

			if (text == null) {
				GameObject child = new("Text");
				RectTransform childRect = child.AddComponent<RectTransform>();
				text = child.AddComponent<TextMeshPro>();

				childRect.SetParent(transform, false);
				childRect.localScale = Vector3.one;
				
				childRect.anchorMin = Vector2.zero;
				childRect.anchorMax = Vector2.one;
				childRect.offsetMax = Vector2.zero;
				childRect.offsetMin = Vector2.zero;

				childRect.hideFlags = HideFlags.NotEditable;
				text.gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
		}

		public TextMeshPro GetText() => text;
	}
}
