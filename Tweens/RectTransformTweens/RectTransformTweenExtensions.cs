using UnityEngine;

namespace Frolics.Tweens.RectTransformTweens {
	public static class RectTransformTweenExtensions {
		public static PropertyTween<RectTransform, Vector2> TweenAnchoredPosition(
			this RectTransform rt,
			Vector2 target,
			float duration
		) {
			return new PropertyTween<RectTransform, Vector2>(
				rt,
				getter: r => r.anchoredPosition,
				setter: (r, v) => r.anchoredPosition = v,
				target: target,
				duration: duration,
				lerp: Vector2.Lerp
			);
		}

		public static PropertyTween<RectTransform, Vector3> TweenLocalScale(
			this RectTransform rt,
			Vector3 target,
			float duration
		) {
			return new PropertyTween<RectTransform, Vector3>(
				rt,
				getter: r => r.localScale,
				setter: (r, v) => r.localScale = v,
				target: target,
				duration: duration,
				lerp: Vector3.Lerp
			);
		}

		public static PropertyTween<RectTransform, Vector3> TweenEulerAngles(
			this RectTransform rt,
			Vector3 target,
			float duration
		) {
			return new PropertyTween<RectTransform, Vector3>(
				rt,
				getter: r => r.eulerAngles,
				setter: (r, v) => r.eulerAngles = v,
				target: target,
				duration: duration,
				lerp: Vector3.Lerp
			);
		}
	}
}
