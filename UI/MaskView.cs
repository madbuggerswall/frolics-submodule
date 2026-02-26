using UnityEngine;

namespace Frolics.UI {
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class MaskView : MonoBehaviour {
		[SerializeField, HideInInspector] private RectTransform rectTransform;
		[SerializeField, HideInInspector] private SpriteRenderer spriteRenderer;
		[SerializeField, HideInInspector] private SpriteMask spriteMask;

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

			if (spriteRenderer == null) {
				// Create child GameObject with RectTransform + SpriteRenderer
				GameObject child = new GameObject("SpriteRenderer");
				RectTransform childRect = child.AddComponent<RectTransform>();
				spriteRenderer = child.AddComponent<SpriteRenderer>();
				spriteRenderer.drawMode = SpriteDrawMode.Sliced;
				spriteRenderer.color = new Color(0f, 0f, 0f, 0f);

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

			if (spriteMask == null) {
				spriteMask = spriteRenderer.gameObject.AddComponent<SpriteMask>();
				spriteMask.maskSource = SpriteMask.MaskSource.SupportedRenderers;
			}
		}

		public RectTransform GetRectTransform() => rectTransform;
		public SpriteRenderer GetSpriteRenderer() => spriteRenderer;
		public SpriteMask GetSpriteMask() => spriteMask;
	}
}
