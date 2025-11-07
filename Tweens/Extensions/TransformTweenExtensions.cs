using Frolics.Tweens.Core;
using Frolics.Tweens.Factory;
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

		public static Tween TweenLocalScale(this Transform tweener, Vector3 target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalScale(tweener, target, duration);
		}

		public static Tween TweenRotation(this Transform tweener, Quaternion target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenRotation(tweener, target, duration);
		}

		public static Tween TweenLocalRotation(this Transform tweener, Quaternion target, float duration) {
			return TweenManager.GetInstance().GetTweenFactory().TweenLocalRotation(tweener, target, duration);
		}

		public static Tween TweenRotateAround(
			this Transform tweener,
			Vector3 axis,
			Vector3 pivot,
			float targetAngle,
			float duration
		) {
			TweenFactory tweenFactory = TweenManager.GetInstance().GetTweenFactory();
			return tweenFactory.TweenRotateAround(tweener, axis, pivot, targetAngle, duration);
		}
	}
}
