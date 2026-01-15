using Frolics.Tweens.Types;
using UnityEngine;

namespace Frolics.Tweens.Accessors {
	internal struct AnchoredPosition : IPropertyAccessor<RectTransform, Vector2> {
		public Vector2 Get(RectTransform tweener) => tweener.anchoredPosition;
		public void Set(RectTransform tweener, Vector2 value) => tweener.anchoredPosition = value;
	}
}
