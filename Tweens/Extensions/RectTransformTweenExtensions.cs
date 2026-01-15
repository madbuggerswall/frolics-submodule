using Frolics.Tweens.Core;
using UnityEngine;

namespace Frolics.Tweens.Extensions {
	public static class RectTransformTweenExtensions {
		public static Tween TweenAnchoredPosition(this RectTransform rt, Vector2 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenAnchoredPosition(rt, target, duration);
		}
	}
}
