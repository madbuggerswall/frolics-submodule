using UnityEngine;

namespace Frolics.Tweens.Extensions {
	public static class RectTransformTweenExtensions {
		public static Tween TweenAnchoredPosition(this RectTransform rt, Vector2 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenAnchoredPosition(rt, target, duration);
		}

		public static Tween TweenLocalScale(this RectTransform rt, Vector3 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalScale(rt, target, duration);
		}

		public static Tween TweenEulerAngles(this RectTransform rt, Vector3 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenEulerAngles(rt, target, duration);
		}
	}
}
