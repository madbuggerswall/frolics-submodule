using Frolics.Tweens.Core;
using TMPro;
using UnityEngine;

namespace Frolics.Tweens.Extensions {
	public static class TextMeshProTweenExtensions {
		public static Tween TweenColor(this TextMeshPro text, Color target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenColor(text, target, duration);
		}

		public static Tween TweenAlpha(this TextMeshPro text, float targetAlpha, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenAlpha(text, targetAlpha, duration);
		}
	}
}
