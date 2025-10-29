using UnityEngine;

namespace Frolics.Tweens.SpriteRendererTweens {
	public static class SpriteRendererTweenExtensions {
		public static PropertyTween<SpriteRenderer, Color> TweenColor(
			this SpriteRenderer spriteRenderer,
			Color target,
			float duration
		) {
			return new PropertyTween<SpriteRenderer, Color>(
				spriteRenderer,
				getter: s => s.color,
				setter: (s, c) => s.color = c,
				target: target,
				duration: duration,
				lerp: Color.Lerp
			);
		}

		public static PropertyTween<SpriteRenderer, float> TweenAlpha(
			this SpriteRenderer sr,
			float targetAlpha,
			float duration
		) {
			return new PropertyTween<SpriteRenderer, float>(
				sr,
				getter: s => s.color.a,
				setter: (s, a) => {
					Color c = s.color;
					c.a = a;
					s.color = c;
				},
				target: targetAlpha,
				duration: duration,
				lerp: Mathf.Lerp
			);
		}
	}
}
