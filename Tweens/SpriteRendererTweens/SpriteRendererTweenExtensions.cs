using UnityEngine;

namespace Frolics.Tweens.SpriteRendererTweens {
	public static class SpriteRendererTweenExtensions {
		public static ColorTween PlayColor(this SpriteRenderer tweener, Color targetColor, float duration) {
			return new ColorTween(tweener, targetColor, duration);
		}
	}
}