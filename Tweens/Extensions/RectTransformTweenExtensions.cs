using UnityEngine;

namespace Frolics.Tweens.RectTransformTweens {
	public static class RectTransformTweenExtensions {
		public static PropertyTween<RectTransform, Vector2> TweenAnchoredPosition(
			this RectTransform rt,
			Vector2 target,
			float duration
		) {
			return TweenManager.GetInstance().GetTweenFactory().TweenAnchoredPosition(rt, target, duration);
		}

		public static PropertyTween<RectTransform, Vector3> TweenLocalScale(
			this RectTransform rt,
			Vector3 target,
			float duration
		) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalScale(rt, target, duration);
		}

		public static PropertyTween<RectTransform, Vector3> TweenEulerAngles(
			this RectTransform rt,
			Vector3 target,
			float duration
		) {
			return TweenManager.GetInstance().GetTweenFactory().TweenEulerAngles(rt, target, duration);
		}
	}
}
