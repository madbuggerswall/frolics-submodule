using UnityEngine;

namespace Frolics.UI {
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class ImageView : MonoBehaviour {
		[SerializeField, HideInInspector] private RectTransform rectTransform;
		[SerializeField, HideInInspector] private SpriteRenderer spriteRenderer;

		private void OnEnable() {
			if (spriteRenderer != null)
				spriteRenderer.enabled = true;
		}

		private void OnDisable() {
			if (spriteRenderer != null)
				spriteRenderer.enabled = false;
		}

		private void Reset() => Initialize();

		private void OnRectTransformDimensionsChange() {
			if (spriteRenderer.size != rectTransform.rect.size)
				spriteRenderer.size = rectTransform.rect.size;
		}

		private void Initialize() {
			if (rectTransform == null)
				rectTransform = GetComponent<RectTransform>();

			if (spriteRenderer == null)
				spriteRenderer = GetComponentInChildren<SpriteRenderer>();

			if (rectTransform == null)
				rectTransform = gameObject.AddComponent<RectTransform>();

			if (spriteRenderer == null) {
				// Create child GameObject with RectTransform + SpriteRenderer
				GameObject child = new GameObject("SpriteRenderer");
				RectTransform childRect = child.AddComponent<RectTransform>();
				spriteRenderer = child.AddComponent<SpriteRenderer>();
				spriteRenderer.drawMode = SpriteDrawMode.Sliced;

				childRect.SetParent(transform, false);
				childRect.localScale = Vector3.one;

				// Set child rect anchor to stretch
				childRect.anchorMin = Vector2.zero;
				childRect.anchorMax = Vector2.one;
				childRect.offsetMax = Vector2.zero;
				childRect.offsetMin = Vector2.zero;

				childRect.hideFlags = HideFlags.NotEditable;
				spriteRenderer.gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
		}

		public RectTransform GetRectTransform() => rectTransform;
		public SpriteRenderer GetSpriteRenderer() => spriteRenderer;
	}
}
