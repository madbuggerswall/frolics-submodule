using UnityEngine;

namespace Frolics.Tweens.SpriteRendererTweens {
	public static class SpriteRendererTweenExtensions {
		public static PropertyTween<SpriteRenderer, Color> TweenColor(
			this SpriteRenderer spriteRenderer,
			Color target,
			float duration
		) {
			return TweenManager.GetInstance().GetTweenFactory().TweenColor(spriteRenderer, target, duration);
		}

		public static PropertyTween<SpriteRenderer, float> TweenAlpha(
			this SpriteRenderer spriteRenderer,
			float targetAlpha,
			float duration
		) {
			return TweenManager.GetInstance().GetTweenFactory().TweenAlpha(spriteRenderer, targetAlpha, duration);
		}
	}
}
