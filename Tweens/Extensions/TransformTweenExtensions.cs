using Frolics.Tweens.Core;
using UnityEngine;

namespace Frolics.Tweens.Extensions {
	public static class TransformTweenExtensions {
		public static Tween TweenPosition(this Transform tweener, Vector3 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenPosition(tweener, target, duration);
		}

		public static Tween TweenPositionX(this Transform tweener, float targetX, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenPositionX(tweener, targetX, duration);
		}

		public static Tween TweenPositionY(this Transform tweener, float targetY, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenPositionY(tweener, targetY, duration);
		}

		public static Tween TweenPositionZ(this Transform tweener, float targetZ, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenPositionZ(tweener, targetZ, duration);
		}

		public static Tween TweenLocalPosition(this Transform tweener, Vector3 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalPosition(tweener, target, duration);
		}

		public static Tween TweenLocalPositionX(this Transform tweener, float targetX, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalPositionX(tweener, targetX, duration);
		}

		public static Tween TweenLocalPositionY(this Transform tweener, float targetY, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalPositionY(tweener, targetY, duration);
		}

		public static Tween TweenLocalPositionZ(this Transform tweener, float targetZ, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalPositionZ(tweener, targetZ, duration);
		}

		public static Tween TweenLocalScale(this Transform tweener, Vector3 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalScale(tweener, target, duration);
		}

		public static Tween TweenRotation(this Transform tweener, Quaternion target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenRotation(tweener, target, duration);
		}

		public static Tween TweenLocalRotation(this Transform tweener, Quaternion target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalRotation(tweener, target, duration);
		}

		public static Tween TweenLocalEulerAngles(this Transform tweener, Vector3 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenEulerAngles(tweener, target, duration);
		}
	}
}
