using UnityEngine;

namespace Frolics.Tweens.RigidbodyTweens {
	public static class RigidbodyTweenExtensions {
		public static PositionTween PlayPosition(this Rigidbody tweener, Vector3 targetPosition, float duration) {
			return new PositionTween(tweener, targetPosition, duration);
		}

		public static PositionXTween PlayPositionX(this Rigidbody tweener, float targetX, float duration) {
			return new PositionXTween(tweener, targetX, duration);
		}

		public static PositionYTween PlayPositionY(this Rigidbody tweener, float targetY, float duration) {
			return new PositionYTween(tweener, targetY, duration);
		}

		public static PositionZTween PlayPositionZ(this Rigidbody tweener, float targetZ, float duration) {
			return new PositionZTween(tweener, targetZ, duration);
		}
	}
}
