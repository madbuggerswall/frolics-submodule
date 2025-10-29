using UnityEngine;

namespace Frolics.Tweens.Extensions {
	public static class SpriteRendererTweenExtensions {
		public static Tween TweenColor(
			this SpriteRenderer spriteRenderer,
			Color target,
			float duration
		) {
			return TweenManager.GetInstance().GetTweenFactory().TweenColor(spriteRenderer, target, duration);
		}

		public static Tween TweenAlpha(
			this SpriteRenderer spriteRenderer,
			float targetAlpha,
			float duration
		) {
			return TweenManager.GetInstance().GetTweenFactory().TweenAlpha(spriteRenderer, targetAlpha, duration);
		}
	}
}
