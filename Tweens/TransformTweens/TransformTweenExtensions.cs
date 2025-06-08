using UnityEngine;

namespace Frolics.Tweens.TransformTweens {
	public static class TransformTweenExtensions {
		public static PositionTween PlayPosition(this Transform tweener, Vector3 targetPosition, float duration) {
			return new PositionTween(tweener, targetPosition, duration);
		}

		public static ScaleTween PlayScale(this Transform tweener, Vector3 targetScale, float duration) {
			return new ScaleTween(tweener, targetScale, duration);
		}

		public static RotationTween PlayRotation(this Transform tweener, Quaternion targetRotation, float duration) {
			return new RotationTween(tweener, targetRotation, duration);
		}

		public static RotateAroundTween PlayRotateAround(
			this Transform tweener,
			Vector3 axis,
			Vector3 pivot,
			float targetAngle,
			float duration
		) {
			return new RotateAroundTween(tweener, axis, pivot, targetAngle, duration);
		}
	}
}
